using System;
using System.Text;

namespace WpfApp13B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // 郵便番号データのコレクション
        public System.Collections.ObjectModel.ObservableCollection<ZipRecord> ZipRecords { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // 郵便番号データのコレクションを初期化
            this.ZipRecords = new System.Collections.ObjectModel.ObservableCollection<ZipRecord>();
            // 関連付け
            listView.DataContext = this.ZipRecords;
        }

        /// <summary>
        /// CSVファイル読み込み
        /// </summary>
        /// <param name="filePath">CSVファイルパス</param>
        private void ReadCsv(string filePath)
        {
            this.ZipRecords.Clear();

            var parser =
                new Microsoft.VisualBasic.FileIO.TextFieldParser(filePath, Encoding.Default);
            using (parser)
            {
                // 「,」区切りのデータとして処理する
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(",");

                try
                {
                    // ファイルの終わりまで繰り返す
                    while (parser.EndOfData == false)
                    {
                        // 一行分読み込み
                        string[] buf = parser.ReadFields();

                        this.ZipRecords.Add(new ZipRecord
                        {
                            Code = buf[0],
                            ZipOld = buf[1],
                            Zip = buf[2],
                            StateKana = buf[3],
                            CityKana = buf[4],
                            TownKana = buf[5],
                            State = buf[6],
                            City = buf[7],
                            Town = buf[8],
                            Flag1 = buf[9],
                            Flag2 = buf[10],
                            Flag3 = buf[11],
                            Flag4 = buf[12],
                            Flag5 = buf[13],
                            Flag6 = buf[14],
                        });

                    }
                }
                catch
                {
                    throw new Exception("CSV読み込みでエラー！");
                }
            }
        }

        private void openItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // フィルタ設定
            dlg.Filter = "CSVファイル(*.csv)|*.csv|テキストファイル(*.txt)|*.txt|全てのファイル(*.*)|*.*";
            // フィルタの1番目(CSV)を選択状態にする
            dlg.FilterIndex = 1;

            // ファイルを開くダイアログ表示
            if (dlg.ShowDialog() == true)
            {
                this.IsEnabled = false;

                // CSV読み込み
                ReadCsv(dlg.FileName);

                this.IsEnabled = true;
            }
        }

        private void exitItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ZipRecords.Clear();
        }
    }
}