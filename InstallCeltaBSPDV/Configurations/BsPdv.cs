using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    internal static class BsPdv {
       
        static public async Task configureBsPdv(EnableConfigurations enable) {

            await Utils.downloadFileTaskAsync(Utils.installBsPdvZip);
            Task.Delay(700).Wait(); //só pra confirmar que realmente terminou o download do arquivo

            await Windows.extractFile(Utils.cInstallBsPdvZip, Utils.cInstall, Utils.installBsPdvZip, enable.checkBoxCopyCetaBSPDV);

            await Windows.enableAllPermissionsForPath(Utils.cInstallPdvCeltabspdv, enable.richTextBoxResults); //as vezes da erro pra fazer a extração se não deixar permissão pra todos
            Task.Delay(700).Wait(); //para ter certeza que já terá extraído a pasta e que já terá dado permissão para todos usuários na pasta. Se não fizer isso, da erro as vezes

            await Windows.movePath(Utils.cInstallPdvCeltabspdv, Utils.cCeltabspdv, enable.richTextBoxResults); //essencial fazer esse processo depois de baixaro arquivo installBsPdv.zip

            await createPdvLink(enable);
            await editMongoCfg(enable); //nessa função já está adicionando permissão para todos usuários na pasta do arquivo. Ele só chega nessa parte quando existe a pasta do arquivo
        }

        private static async Task createPdvLink(EnableConfigurations enable) {
            string pdvPath = "C:\\CeltaBSPDV\\CeltaWare.CBS.PDV.UI.exe";
            string startupUiPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.UI.exe";
            string startupPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\CeltaWare.CBS.PDV.exe";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CeltaWare.CBS.PDV.exe";
            string desktopUiPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CeltaWare.CBS.PDV.UI.exe";

            if(!File.Exists(pdvPath)) {
                enable.richTextBoxResults.Text += $"Não foi possível encontrar o arquivo {pdvPath}. Iniciando download do installbspdv.zip para fazer as configurações das pastas\n";
                enable.richTextBoxResults.Text += "Iniciando download do installBsPdv.zip\n\n";
                await Utils.downloadFileTaskAsync(Utils.installBsPdvZip);
                await Windows.extractFile(Utils.cInstallBsPdvZip, Utils.cInstall, "installbspdv.zip");

                Task.Delay(7000).Wait();
                await Windows.movePath(Utils.cInstallPdvCeltabspdv, Utils.cCeltabspdv, enable.richTextBoxResults);
                Task.Delay(7000).Wait();
                await createPdvLink(enable);
            }

            if(File.Exists(startupPath) && File.Exists(startupUiPath)) {
                File.Delete(startupUiPath);
                enable.checkBoxPdvLink.Checked = true;
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
                enable.richTextBoxResults.Text += "O PDV já está na pasta de inicialização automática do windows\n\n";
            } else if(File.Exists(startupPath) || File.Exists(startupUiPath)) {
                enable.checkBoxPdvLink.Checked = true;
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
                enable.richTextBoxResults.Text += "O PDV já está na pasta de inicialização automática do windows\n\n";
            } else {
                try {
                    await Task.Run(() => File.CreateSymbolicLink(startupPath, pdvPath));
                    enable.checkBoxPdvLink.Checked = true;
                    //enable.checkBoxPdvLink.ForeColor = Color.Green;
                    enable.richTextBoxResults.Text += "O atalho do PDV foi adicionado na pasta de inicialização do windows\n\n";
                } catch(Exception ex) {
                    MessageBox.Show($"Erro para criar o atalho do PDV: {ex.Message}");
                }
            }

            if(File.Exists(desktopPath) && File.Exists(desktopUiPath)) {
                File.Delete(startupUiPath);
                enable.richTextBoxResults.Text += "O PDV já está na área de trabalho\n\n";
                enable.checkBoxPdvLink.Checked = true;
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else if(File.Exists(desktopPath) || File.Exists(desktopUiPath)) {
                enable.richTextBoxResults.Text += "O PDV já está na área de trabalho\n\n";
                enable.checkBoxPdvLink.Checked = true;
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
            } else {
                await Task.Run(() => File.CreateSymbolicLink(desktopPath, pdvPath));
                enable.checkBoxPdvLink.Checked = true;
                //enable.checkBoxPdvLink.ForeColor = Color.Green;
                enable.richTextBoxResults.Text += "O atalho do PDV foi adicionado na área de trabalho\n\n";
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

                await Windows.enableAllPermissionsForPath(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\MongoDB\\Server\\4.0\\bin", enable.richTextBoxResults);

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
