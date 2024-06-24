using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class PinPads {

        /// <summary>
        /// e-mail e senha onde estão os arquivos de download
        ///email: suporteceltaware@hotmail.com
        ///senha: Celta@123
        /// </summary>

        private DownloadFilesForm downloadFilesForm;

        public PinPads(DownloadFilesForm downloadFilesForm) {
            this.downloadFilesForm = downloadFilesForm;
            addPinPadsInUrlsDictionary();
            addItemsInCheckedListBoxPinPads();
        }

        #region Utilities List and names
        private List<string> pinPads = new() { gertecPPC930, ingenicoIPP320 };

        private const string gertecPPC930 = "Gertec PPC930-9320";
        private const string ingenicoIPP320 = "Ingenico IPP320";

        #endregion
        private void addItemsInCheckedListBoxPinPads() {
            foreach(string utility in pinPads) {
                downloadFilesForm.checkedListBoxPinPads.Items.Add(utility);
            }
            downloadFilesForm.checkedListBoxPinPads.Height = downloadFilesForm.checkedListBoxPinPads.Items.Count * downloadFilesForm.checkedListBoxPinPads.ItemHeight + 5;
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
        private void addPinPadsInUrlsDictionary() {
            downloadFilesForm.urlsDownloadDictionary.Add(
                gertecPPC930,
                new Dictionary<string, string>() { {
                        $"{gertecPPC930}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                ingenicoIPP320,
                new Dictionary<string, string>() { {
                        $"{ingenicoIPP320}.exe",
                        "http://187.35.140.227/downloads/lastversion/Programas"} });
        }

    }
}
