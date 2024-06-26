﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallCeltaBSPDV.Forms.DownloadFiles;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class SATs {

        /// <summary>
        /// e-mail e senha onde estão os arquivos de download
        ///email: suporteceltaware@hotmail.com
        ///senha: Celta@123
        /// </summary>
        
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
        private const string satControlId = "SAT control ID";
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
                        "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satBematech,
                new Dictionary<string, string>() { {
                        $"{satBematech}.zip",
                       "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satDimep,
                new Dictionary<string, string>() { {
                        $"{satDimep}.zip",
                       "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satTanca,
                new Dictionary<string, string>() { {
                        $"{satTanca}.zip",
                      "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satGertec,
                new Dictionary<string, string>() { {
                        $"{satGertec}.zip",
                     "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satElgin,
                new Dictionary<string, string>() { {
                        $"{satElgin}.zip",
                     "http://187.35.140.227/downloads/lastversion/Programas"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                satControlId,
                new Dictionary<string, string>() { {
                        $"{satControlId}.zip",
                     "http://187.35.140.227/downloads/lastversion/Programas"} });
        }
    }
}
