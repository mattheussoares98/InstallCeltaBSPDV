using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles
{
    internal class Utilities
    {

        /// <summary>
        /// e-mail e senha onde estão os arquivos de download
        ///email: suporteceltaware@hotmail.com
        ///senha: Celta@123
        /// </summary>

        private DownloadFilesForm downloadFilesForm;

        public Utilities(DownloadFilesForm downloadFilesForm)
        {
            this.downloadFilesForm = downloadFilesForm;
            addUtilitiesInUrlsDictionary();
            addItemsInCheckedListBoxUtilities();
        }

        #region Utilities List and names
        private List<string> utilities = new() { ultraVnc, wnb, teamViewer, manualPDV, driverBooster, winRar, anydesk, layoutKeyboard };
        private const string ultraVnc = "Ultra VNC";
        private const string wnb = "WNB";
        private const string teamViewer = "TeamViewer";
        private const string manualPDV = "Manual do PDV";
        private const string driverBooster = "Driver Booster";
        private const string winRar = "Win rar";
        private const string anydesk = "Anydesk";
        private const string layoutKeyboard = "Layout teclado";
        #endregion
        private void addItemsInCheckedListBoxUtilities()
        {
            foreach (string utility in utilities)
            {
                downloadFilesForm.checkedListBoxUtilities.Items.Add(utility);
            }
            downloadFilesForm.checkedListBoxUtilities.Height = downloadFilesForm.checkedListBoxUtilities.Items.Count * downloadFilesForm.checkedListBoxUtilities.ItemHeight + 5;
        }

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
        private void addUtilitiesInUrlsDictionary()
        {
            ///MUITO IMPORTANTE
            ///no dictionary que é criado dentro do .Add, precisa ter como chave o nome do arquivo com a extensão

            #region Add utilities
            downloadFilesForm.urlsDownloadDictionary.Add(
                ultraVnc,
                new Dictionary<string, string>() { {
                        $"{ultraVnc}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                winRar,
                new Dictionary<string, string>() { {
                        $"{winRar}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                anydesk,
                new Dictionary<string, string>() { {
                        $"{anydesk}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                wnb,
                new Dictionary<string, string>() { {
                        $"{wnb}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });


            downloadFilesForm.urlsDownloadDictionary.Add(
               teamViewer,
               new Dictionary<string, string>() { {
                       $"{teamViewer}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                manualPDV,
                new Dictionary<string, string>() { {
                        $"{manualPDV}.pdf",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                driverBooster,
                new Dictionary<string, string>() { {
                        $"{driverBooster}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                layoutKeyboard,
                new Dictionary<string, string>() { {
                        $"{layoutKeyboard}.zip",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            #endregion
        }

    }
}
