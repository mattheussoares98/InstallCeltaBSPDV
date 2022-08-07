using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallCeltaBSPDV.Forms.DownloadFiles;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class SATs {
        DownloadFilesForm downloadFilesForm;

        public SATs(DownloadFilesForm downloadFilesForm) {
            this.downloadFilesForm = downloadFilesForm;
            addSatsInUrlsDictionary();
            addItemsInCheckedListBoxSats();
        }

        #region Sats List and names
        private List<string> sats = new() { satSweda, satBematech, satDimep, satTanca, satGertec, satElgin, satControlId };
        private const string satSweda = "SAT sweda";
        private const string satBematech = "SAT bematech";
        private const string satDimep = "SAT dimep";
        private const string satTanca = "SAT tanca";
        private const string satGertec = "SAT gertec";
        private const string satElgin = "SAT elgin";
        private const string satControlId = "SAT control-id";
        #endregion

        private void addItemsInCheckedListBoxSats() {
            foreach(string sat in sats) {
                downloadFilesForm.checkedListBoxSats.Items.Add(sat);
            }
            downloadFilesForm.checkedListBoxSats.Height = downloadFilesForm.checkedListBoxSats.Items.Count * downloadFilesForm.checkedListBoxSats.ItemHeight + 5;
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
        private void addSatsInUrlsDictionary() {
            downloadFilesForm.urlsDownloadDictionary.Add(
                satSweda,
                new Dictionary<string, string>() { {
                        $"{satSweda}.zip",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21127&authkey=AMpAPUVwDFnfo2Y"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satBematech,
                new Dictionary<string, string>() { {
                        $"{satBematech}.zip",
                       "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21132&authkey=AGHqTQ7c13x9i00"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satDimep,
                new Dictionary<string, string>() { {
                        $"{satDimep}.zip",
                       "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21133&authkey=AHJwJFJyBClsVK4"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satTanca,
                new Dictionary<string, string>() { {
                        $"{satTanca}.zip",
                      "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21134&authkey=AF0bY9tOUg5wSlo"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satGertec,
                new Dictionary<string, string>() { {
                        $"{satGertec}.zip",
                     "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21135&authkey=AC65XCxQIaOxHNI"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satElgin,
                new Dictionary<string, string>() { {
                        $"{satElgin}.zip",
                     "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21136&authkey=AAH3AW16tLoTvuw"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satControlId,
                new Dictionary<string, string>() { {
                        $"{satControlId}.zip",
                     "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21137&authkey=AKrDV9xXc6_KAw4"} });
        }
    }
}
