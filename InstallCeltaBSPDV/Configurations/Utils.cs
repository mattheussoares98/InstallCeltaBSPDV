using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    public static class Utils {
        public static readonly string cInstall = "C:\\Install";
        public static readonly string cInstallBsPdvZip = "C:\\Install\\installbspdv.zip";
        public static readonly string installBsPdvZip = "installbspdv.zip";
        public static readonly string cInstallPdvCeltabspdv = "C:\\Install\\PDV\\CeltaBSPDV";
        public static readonly string cCeltabspdv = "C:\\CeltaBSPDV";

        static EnableConfigurations enableConfigurations = new EnableConfigurations();
        public static async Task downloadFileTaskAsync(string fileName, string uri = "http://177.103.179.36/downloads/lastversion/") {
            HttpClient client = new HttpClient();

            string destinyPath = "C:\\Install";

            if(!Directory.Exists(destinyPath)) {
                Directory.CreateDirectory(destinyPath);
            }

            string fileNamePath = destinyPath + "\\" + fileName;

            #region download files
            if(!File.Exists(fileNamePath)) {
                enableConfigurations.richTextBoxResults.Text += "Baixando o " + fileName + ". Dependendo da velocidade da internet, esse processo pode ser demorado\n\n";
                //só tenta baixar o arquivo se ele não existir ainda
                try {
                    using(var s = await client.GetStreamAsync(uri)) {
                        using(var fs = new FileStream(fileNamePath, FileMode.CreateNew)) {
                            await s.CopyToAsync(fs);
                        }
                    }
                } catch(Exception ex) {
                    MessageBox.Show("Erro para baixar o arquivo: " + ex.Message);
                }
                enableConfigurations.richTextBoxResults.Text += fileName + " baixado com sucesso\n\n";
            } else {
                enableConfigurations.richTextBoxResults.Text += $"O {fileName} já foi baixado\n\n";
            }
            #endregion
        }

    }
}
