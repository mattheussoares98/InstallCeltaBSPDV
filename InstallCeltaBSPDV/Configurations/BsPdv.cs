﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal static class BsPdv {

        static public async Task configureBsPdv(EnableConfigurations enable) {

            await Download.downloadFileTaskAsync(Download.installBsPdvZip);
            Task.Delay(700).Wait(); //só pra confirmar que realmente terminou o download do arquivo

            await Windows.extractFile(Download.cInstallBsPdvZip, Download.cInstall, Download.installBsPdvZip, enable.checkBoxCopyCetaBSPDV);

            await Windows.enableAllPermissionsForPath(Download.cInstallPdvCeltabspdv, enable); //as vezes da erro pra fazer a extração se não deixar permissão pra todos
            Task.Delay(700).Wait(); //para ter certeza que já terá extraído a pasta e que já terá dado permissão para todos usuários na pasta. Se não fizer isso, da erro as vezes

            await Windows.movePath(Download.cInstallPdvCeltabspdv, Download.cCeltabspdv, enable); //essencial fazer esse processo depois de baixaro arquivo installBsPdv.zip

            await verifyPdvPathExists(enable);

            try {
                await createStartupLink();
                await createDesktopLink();
                enable.checkBoxPdvLink.Checked = true;
            } catch(Exception ex) { MessageBox.Show($"Erro para criar o atalho do PDV: {ex.Message}"); }

            await editMongoCfg(enable); //nessa função já está adicionando permissão para todos usuários na pasta do arquivo. Ele só chega nessa parte quando existe a pasta do arquivo
        }

        private static async Task verifyPdvPathExists(EnableConfigurations enable) {
            if(!File.Exists(pdvPath)) {
                enable.richTextBoxResults.Text += $"Não foi possível encontrar o arquivo {pdvPath}. Iniciando download do installbspdv.zip para fazer as configurações das pastas\n";

                await Download.downloadFileTaskAsync(Download.installBsPdvZip);
                await Windows.extractFile(Download.cInstallBsPdvZip, Download.cInstall, "installbspdv.zip");

                Task.Delay(7000).Wait();
                await Windows.movePath(Download.cInstallPdvCeltabspdv, Download.cCeltabspdv, enable);
                Task.Delay(7000).Wait();
            }
        }
        #region directories
        private static string pdvPath = "C:\\CeltaBSPDV\\CeltaWare.CBS.PDV.UI.exe";
        private static string startupUiPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.UI.exe";
        private static string startupPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.exe";
        private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CeltaWare.CBS.PDV.exe";
        private static string desktopUiPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CeltaWare.CBS.PDV.UI.exe";
        #endregion
        private static async Task createStartupLink() {
            if(File.Exists(startupPath) && File.Exists(startupUiPath)) {
                File.Delete(startupUiPath);
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else if(File.Exists(startupPath) || File.Exists(startupUiPath)) {
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else {
                try {
                    await Task.Run(() => File.CreateSymbolicLink(startupPath, pdvPath));
                    //enable.checkBoxPdvLink.ForeColor = Color.Green;
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para criar o atalho do PDV: {ex.Message}");
                }
            }
        }
        private static async Task createDesktopLink() {

            if(File.Exists(desktopPath) && File.Exists(desktopUiPath)) {
                File.Delete(startupUiPath);
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else if(File.Exists(desktopPath) || File.Exists(desktopUiPath)) {
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else {
                try {
                await Task.Run(() => File.CreateSymbolicLink(desktopPath, pdvPath));
                }catch(Exception ex) {
                    MessageBox.Show($"Erro para criar o atalho na área de trabalho: {ex.Message}");
                }
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            }
        }

        private static async Task editMongoCfg(EnableConfigurations enable) {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string mongoBin = programFiles += "\\MongoDB\\Server\\4.0\\bin";
            string mongoConfig = mongoBin + "\\mongod.cfg";

            if(!File.Exists(mongoConfig)) {
                //richTextBoxResults.Text += $"O {mongoConfig} não existe. Clique em 'YES' para tentar abrir o instalador do PDV\n\n";
                DialogResult dialogResult = MessageBox.Show($"O {mongoConfig} não existe\n\nClique em 'OK' para abrir o instalador do PDV e habilitar o acesso remoto ao banco de dados\n\n", "ALERTA!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                openInstallMongoDb();
                await editMongoCfg(enable);
            }

            try {
                StreamReader sr = File.OpenText(mongoConfig);
                string? textoDoArquivo = sr.ReadToEnd();
                sr.Close();

                if(textoDoArquivo.Contains("0.0.0.0")) {

                    enable.checkBoxEnableRemoteAcces.Checked = true;
                    //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                    enable.checkBoxInstallMongo.Checked = true;
                    //enable.checkBoxInstallMongo.ForeColor = Color.Green;
                    return;
                }

                await Windows.enableAllPermissionsForPath(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\MongoDB\\Server\\4.0\\bin", enable);

                string newText = textoDoArquivo.Replace("127.0.0.1", "0.0.0.0");

                Task.Delay(3000).Wait();

                File.WriteAllText(mongoConfig, newText); //caso já tenha um arquivo com o mesmo nome, ele sobrescreve com o texto que está no segundo parâmetro

                enable.checkBoxEnableRemoteAcces.Checked = true;
                //enable.checkBoxEnableRemoteAcces.ForeColor = Color.Green;
                enable.checkBoxInstallMongo.Checked = true;
                //enable.checkBoxInstallMongo.ForeColor = Color.Green;

            } catch(Exception e) {
                MessageBox.Show("Erro para ler os dados do arquivo: " + e.Message);
                Console.WriteLine(e.Message);
            }


        }

        private static void openInstallMongoDb() {
            if(File.Exists(@"C:\Install\PDV\Database\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed.msi")) {
                var mongoPath = new ProcessStartInfo("cmd", "/c C:\\Install\\PDV\\Database\\mongodb-win32-x86_64-2008plus-ssl-4.0.22-signed.msi");

                mongoPath.CreateNoWindow = true;
                Process.Start(mongoPath);
            }
        }
    }
}
