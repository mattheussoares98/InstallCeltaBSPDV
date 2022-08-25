using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class Utilities {

        private DownloadFilesForm downloadFilesForm;

        public Utilities(DownloadFilesForm downloadFilesForm) {
            this.downloadFilesForm = downloadFilesForm;
            addUtilitiesInUrlsDictionary();
            addItemsInCheckedListBoxUtilities();
        }

        #region Utilities List and names
        private List<string> utilities = new() { ultraVnc, wnb, webConfigSat, teamViewer, layoutKeyboard, manualPDV, driverBooster, winRar };
        private const string ultraVnc = "Ultra VNC";
        private const string wnb = "WNB";
        private const string webConfigSat = "WebConfig SAT";
        private const string teamViewer = "Team Viewer";
        private const string manualPDV = "Manual do PDV";
        private const string driverBooster = "Driver Booster";
        private const string winRar = "Win rar";
        private const string anydesk = "Anydesk";
        #endregion
        private void addItemsInCheckedListBoxUtilities() {
            foreach(string utility in utilities) {
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
        private void addUtilitiesInUrlsDictionary() {
            ///MUITO IMPORTANTE
            ///no dictionary que é criado dentro do .Add, precisa ter como chave o nome do arquivo com a extensão

            #region Add utilities
            downloadFilesForm.urlsDownloadDictionary.Add(
                ultraVnc,
                new Dictionary<string, string>() { {
                        $"{ultraVnc}.exe",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21109&authkey=ALOZWCEuNXdXNGA" } });

            downloadFilesForm.urlsDownloadDictionary.Add(
                wnb,
                new Dictionary<string, string>() { {
                        $"{wnb}.exe",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21111&authkey=AFyJjBYHrvfDcwI"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                webConfigSat,
                new Dictionary<string, string>() { {
                        $"web.config", //como precisa ter esse nome, troquei direto aqui
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21108&authkey=AOp9tqDqLcnjeZw"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
               teamViewer,
               new Dictionary<string, string>() { {
                       $"{teamViewer}.exe",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21114&authkey=AD1INLbVBnxmXoM"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                layoutKeyboard,
                new Dictionary<string, string>() { {
                        $"{layoutKeyboard}.zip",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21112&authkey=AJLNQmrohGWWneE"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                manualPDV,
                new Dictionary<string, string>() { {
                        $"{manualPDV}.pdf",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21115&authkey=ABymY2rTS2vILJ4&em=2"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                driverBooster,
                new Dictionary<string, string>() { {
                        $"{driverBooster}.exe",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21113&authkey=AGU_LMBSTCYiJAE"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                winRar,
                new Dictionary<string, string>() { {
                        $"{winRar}.exe",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21110&authkey=AMo4Id4dJ3NIhWg"} });
            #endregion
        }

    }
}
