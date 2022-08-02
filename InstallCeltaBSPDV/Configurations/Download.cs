using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Configurations {
    public static class Download {
        public static readonly string cInstall = "C:\\Install";
        public static readonly string cInstallBsPdvZip = "C:\\Install\\installbspdv.zip";
        public static readonly string installBsPdvZip = "installbspdv.zip";
        public static readonly string cInstallPdvCeltabspdv = "C:\\Install\\PDV\\CeltaBSPDV";
        public static readonly string cCeltabspdv = "C:\\CeltaBSPDV";

        public static async Task downloadFileTaskAsync(string fileName, EnableConfigurations enable, string uriDownload) {
            HttpClient client = new HttpClient();

            string destinyPath = "C:\\Install";

            if(!Directory.Exists(destinyPath)) {
                Directory.CreateDirectory(destinyPath);
            }

            string fileNamePath = destinyPath + "\\" + fileName;

            #region download files
            if(!File.Exists(fileNamePath)) {
                enable.richTextBoxResults.Text += "Baixando o " + fileName + ". Dependendo da velocidade da internet, esse processo pode ser demorado\n\n";
                //só tenta baixar o arquivo se ele não existir ainda

                try {
                    using(var s = await client.GetStreamAsync(uriDownload)) {
                        using(var fs = new FileStream(fileNamePath, FileMode.CreateNew)) {
                            await s.CopyToAsync(fs);
                        }
                    }
                    enable.richTextBoxResults.Text += fileName + " baixado com sucesso\n\n";
                } catch(Exception ex) {
                    MessageBox.Show("Erro para fazer o download: " + ex.Message);
                    enable.richTextBoxResults.Text +=
                    "Erro para baixar o arquivo. \nErro: " + ex.Message + "\nIniciando download novamente\n\n";

                    if(File.Exists(cInstall + $"\\{fileName}")) { //teoricamente iniciou o download mas deu erro, por isso precisa apagar o arquivo pra tentar efetuar o download novamente
                        File.Delete(cInstall + $"\\{fileName}");
                    }
                    await downloadFileTaskAsync(fileName, enable, uriDownload);
                }
            } else {
                enable.richTextBoxResults.Text += $"O {fileName} já foi baixado\n\n";
            }
            #endregion

        }

    }
}
