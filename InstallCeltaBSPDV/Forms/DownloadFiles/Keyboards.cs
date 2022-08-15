using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallCeltaBSPDV.Forms.DownloadFiles {
    internal class Keyboards {

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
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21143&authkey=AAebnjHcw7GS__U"} });

            downloadFilesForm.urlsDownloadDictionary.Add(
                gertec,
                new Dictionary<string, string>() { {
                        $"{gertec}.zip",
                        "https://onedrive.live.com/download?cid=D4CEA33D5404A268&resid=D4CEA33D5404A268%21142&authkey=AP1t5Du-H9FqjEk"} });
        }

        private void addItemsInCheckedListBoxPinPads() {
            foreach(string utility in keyboards) {
                downloadFilesForm.checkedListBoxKeyboards.Items.Add(utility);
            }
            downloadFilesForm.checkedListBoxKeyboards.Height = downloadFilesForm.checkedListBoxKeyboards.Items.Count * downloadFilesForm.checkedListBoxKeyboards.ItemHeight + 5;
        }
    }
}
