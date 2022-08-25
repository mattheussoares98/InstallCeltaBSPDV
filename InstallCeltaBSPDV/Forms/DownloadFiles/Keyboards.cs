using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class Keyboards {

        /// <summary>
        /// e-mail e senha onde estão os arquivos de download
        ///email: suporteceltaware@hotmail.com
        ///senha: Celta@123
        /// </summary>

        private DownloadFilesForm downloadFilesForm;

        public Keyboards(DownloadFilesForm downloadFilesForm) {
            this.downloadFilesForm = downloadFilesForm;
            addPinPadsInUrlsDictionary();
            addItemsInCheckedListBoxPinPads();
        }

        #region Utilities List and names
        private List<string> keyboards = new() { smak, gertec };

        private const string smak = "Teclado Smak";
        private const string gertec = "Teclado Gertec";

        #endregion

        private void addPinPadsInUrlsDictionary() {
            downloadFilesForm.urlsDownloadDictionary.Add(
                smak,
                new Dictionary<string, string>() { {
                        $"{smak}.zip",
                        "https://onedrive.live.com/download?cid=4ECE55D0B3C830E2&resid=4ECE55D0B3C830E2%21140&authkey=ALqH3YBSihorG7Y"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                gertec,
                new Dictionary<string, string>() { {
                        $"{gertec}.zip",
                        "https://onedrive.live.com/download?cid=4ECE55D0B3C830E2&resid=4ECE55D0B3C830E2%21139&authkey=AIp32ceFHQ6EuXs"} });
        }

        private void addItemsInCheckedListBoxPinPads() {
            foreach(string utility in keyboards) {
                downloadFilesForm.checkedListBoxKeyboards.Items.Add(utility);
            }
            downloadFilesForm.checkedListBoxKeyboards.Height = downloadFilesForm.checkedListBoxKeyboards.Items.Count * downloadFilesForm.checkedListBoxKeyboards.ItemHeight + 5;
        }
    }
}
