using InstallCeltaBSPDV.Configurations;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.DownloadFiles
{
    public class Download
    {
        private readonly EnableConfigurations enable = new();

        public Download(EnableConfigurations enable)
        {
            this.enable = enable;
        }

        public static readonly string cInstall = "C:\\Install";
        public static readonly string cInstallBsPdvZip = "C:\\Install\\installbspdv.zip";
        public static readonly string installBsPdvZip = "installbspdv.zip";
        public static readonly string cInstallPdvCeltabspdv = "C:\\Install\\PDV\\CeltaBSPDV";
        public static readonly string cCeltabspdv = "C:\\CeltaBSPDV";

        public async Task downloadFileTaskAsync(string fileName, string uriDownload, string destinyPath = "C:\\Install")
        {
            HttpClient client = new HttpClient();

            if (!Directory.Exists(destinyPath))
            {
                Directory.CreateDirectory(destinyPath);
            }

            string fileNamePath = destinyPath + "\\" + fileName;

            #region download files
            if (!File.Exists(fileNamePath))
            {
                enable.richTextBoxResults.Text += "Baixando o " + fileName + ". Dependendo da velocidade da internet, esse processo pode ser demorado\n\n";
                //só tenta baixar o arquivo se ele não existir ainda

                try
                {
                    using (var s = await client.GetStreamAsync(uriDownload + "/" + fileName))
                    {
                        using (var fs = new FileStream(fileNamePath, FileMode.CreateNew))
                        {
                            await s.CopyToAsync(fs);
                        }
                    }
                    enable.richTextBoxResults.Text += fileName + " baixado com sucesso\n\n";
                }
                catch (Exception ex)
                {
                    DialogResult dialogResult = MessageBox.Show($"Erro para fazer o download através do link: {uriDownload}/{fileName}\nErro:" + ex.Message + "\n\nDESEJA TENTAR FAZER O DOWNLOAD NOVAMENTE?", "Efetuar download", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {

                        if (File.Exists(cInstall + $"\\{fileName}"))
                        { //teoricamente iniciou o download mas deu erro, por isso precisa apagar o arquivo pra tentar efetuar o download novamente
                            File.Delete(cInstall + $"\\{fileName}");
                        }
                        await downloadFileTaskAsync(fileName, uriDownload);
                    }
                }
            }
            else
            {
                enable.richTextBoxResults.Text += $"O {fileName} já foi baixado\n\n";
            }

            if (fileNamePath.Contains(".zip") || fileNamePath.Contains(".rar"))
            {
                await new Windows(enable).extractFile(fileNamePath, destinyPath, null, null);
            }
            #endregion

        }

    }
}
