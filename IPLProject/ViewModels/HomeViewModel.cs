using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;

namespace IPLProject.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public ICommand PrintCommand { get; }
        public ICommand PreviewCommand { get; }

        public HomeViewModel()
        {
            PrintCommand = new ViewModelCommand(ExecutePrintCommand, CanExecutePrintCommand);
        }

        private bool CanExecutePrintCommand(object obj)
        {
            // 품목ID, 생산일자 등이 비어있으면 에러를 반환할 예정.
            {
                return true;
            }
        }

        private void ExecutePrintCommand(object obj)
        {
            //기존에 XAML 및 cs 파일에 종속된 Click 이벤트를
            //MVVM 패턴 Binding에 적용하기 위해 ViewModel에 옮김.

            // 바코드 내용 -> 추후 라벨 타입 및 Database에서 수신하는 넘버링에 따른 변수로 변경해야됨.
            string barcodeContent = "TJLC-40-23043055";

            // 바코드 생성
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE; // 2D 바코드(QR CODE) 형식 선택
            barcodeWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = 50, // 바코드의 너비 조정
                Height = 50, // 바코드의 높이 조정
                Margin = 0, // 텍스트와 바코드 간의 여백 설정
                PureBarcode = true // 텍스트를 숨김
            };
            Bitmap barcodeBitmap = new Bitmap(barcodeWriter.Write(barcodeContent));

            // 프린터 설정
            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PrinterSettings.PrinterName = "Intermec PD43 (203 dpi)"; // 프린터 이름 설정

            printDocument.PrintPage += (printSender, printEventArgs) =>
            {
                printEventArgs.Graphics.DrawImage(barcodeBitmap, new Point(5, 2)); // 바코드 위치 조정
                printEventArgs.Graphics.DrawString(barcodeContent, new Font("Arial", 8), Brushes.Black, new Point(45, 12)); // 문자열 위치 조정

            };

            // 프린트 실행 -> 추후 일일 생산량에 맞춘 수량만큼 발행하도록 변경...
            printDocument.Print();

            // 메모리 해제
            barcodeBitmap.Dispose();
        }
    }
}
