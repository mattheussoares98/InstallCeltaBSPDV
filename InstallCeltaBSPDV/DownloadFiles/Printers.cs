using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class Printers {

        /// <summary>
        /// e-mail e senha onde estão os arquivos de download
        ///email: suporteceltaware@hotmail.com
        ///senha: Celta@123
        /// </summary>

        DownloadFilesForm downloadFilesForm;
        public Printers(DownloadFilesForm downloadFiles) {
            this.downloadFilesForm = downloadFiles;
            addItemsInCheckedListBoxPrinters();
            addPrintersInUrlsDictionary();
        }
        private void addItemsInCheckedListBoxPrinters() {
            foreach(string printer in printers) {
                downloadFilesForm.checkedListBoxPrinters.Items.Add(printer);
            }
            downloadFilesForm.checkedListBoxPrinters.Height = downloadFilesForm.checkedListBoxPrinters.Items.Count * downloadFilesForm.checkedListBoxPrinters.ItemHeight + 5;
        }

        #region Printers List and names
        private List<string> printers = new() { epsonTMT20, epsonTMT20x, epsonTMT88v, bematechMP4200, swedaSI300S, swedaSI300SIX, darumaDR700, darumaDR800, elginI9, tancatp550 };

        private const string epsonTMT20 = "Epson TMT20";
        private const string epsonTMT20x = "Epson TMT20x";
        private const string epsonTMT88v = "Epson TMT88v";
        private const string bematechMP4200 = "Bematech MP4200";
        private const string swedaSI300S = "Sweda SI300 S";
        private const string swedaSI300SIX = "Sweda SI300 SIX";
        private const string darumaDR700 = "Daruma DR700";
        private const string darumaDR800 = "Daruma DR800";
        private const string elginI9 = "Elgin i9";
        private const string tancatp550 = "Tanca TP550";
        #endregion

        /// <summary>
        /// logo que inicia a aplicação: 
        /// 1- Adiciona no Dictionary a chave "nome do arquivo"
        /// 2- Adiciona outro dictionary dentro dele com:
        /// 2.1- (nome do arquivo + extensão) na Key
        /// 2.2- Url de download no Value
        /// 
        /// Precisei fazer um dictionary dentro do outro porque precisa das seguintes informações: 
        /// 1- Nome do que o usuário selecionou
        /// 2- Nome do arquivo com extensão (sem a extensão o arquivo vem sem extensão e não da pra abrir ele a não ser que fique alterando a extensão manualmente) 
        /// 3- Url de download
        /// 
        /// Quando clica pra baixar os itens, a aplicação verifica todos itens que estão selecionados e insere eles na lista "selectedItemsToDownload"
        /// 
        /// A aplicação percorre o urlsDownloadDictionary através dos valores que estão no "selectedItemsToDownload", vai pegando o  nome do arquivo com a extensão (Keys) e o valor dele (urls) pra efetuar os downloads 
        /// </summary>
        private void addPrintersInUrlsDictionary() {
            downloadFilesForm.urlsDownloadDictionary.Add(
                epsonTMT20,
                new Dictionary<string, string>() { {
                        $"{epsonTMT20}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    epsonTMT20x,
                    new Dictionary<string, string>() { {
                        $"{epsonTMT20x}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    epsonTMT88v,
                    new Dictionary<string, string>() { {
                        $"{epsonTMT88v}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    bematechMP4200,
                    new Dictionary<string, string>() { {
                        $"{bematechMP4200}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    swedaSI300S,
                    new Dictionary<string, string>() { {
                        $"{swedaSI300S}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    swedaSI300SIX,
                    new Dictionary<string, string>() { {
                        $"{swedaSI300SIX}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    darumaDR700,
                    new Dictionary<string, string>() { {
                        $"{darumaDR700}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    darumaDR800,
                    new Dictionary<string, string>() { {
                        $"{darumaDR800}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    elginI9,
                    new Dictionary<string, string>() { {
                        $"{elginI9}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                    tancatp550,
                    new Dictionary<string, string>() { {
                        $"{tancatp550}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });
        }
    }
}
