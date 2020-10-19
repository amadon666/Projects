{$reference 'System.Windows.Forms.dll'}
{$reference 'System.Drawing.dll'}
uses System, System.Windows.Forms, System.Drawing, System.IO;
uses System.Globalization;
uses System.Collections.Generic;
const VERSION_APP = '1.4.0.0';
const BACKGROUND_CONSTANT = 'I:\PROG__\HTML&CSS\Разработки 2019\background_style.png';
const BLACK_COLOR = Color.Black;
const TRSP_COLOR = Color.Transparent;
var f:= new Form();
    tbSourceNumber:= new TextBox();
    sourceNumberLabel:= new &Label();
    cbBase:= new ComboBox();
    system_2_Label:= new &Label();
    tb2:= new TextBox();
    system_8_Label:= new &Label();
    system_16_Label:= new &Label();
    tb8:= new TextBox();
    baseLabel:= new &Label();
    panel_left:= new Panel();
    panel_right:= new Panel();
    OperationLabel:= new &Label();
    tbSecond:= new TextBox();
    tbFirst:= new TextBox();
    SecondNumLabel:= new &Label();
    FirstNumLabel:= new &Label();
    bitwiseLabel:= new &Label();
    cbBitwise:= new ComboBox();
    tbResult:= new TextBox();
    ResultLabel:= new &Label();
    tb16:= new TextBox();
/// Содержит необходимые функции для перевода чисел в другие системы счисления
type SystemNumerationAPI = static class
   /// Рекурсивный перевод десятичного числа в двоичную СС
   public static procedure DecToBinRec(num: integer);
   begin
     if num >= 2 then DecToBinRec(num div 2);
     Write(num mod 2);
   end;
   /// Перевод десятичного числа в двоичную СС  
   public static function DecToBin(num: double): double;
      // Массив для хранения остатков от деления
      var arr: array[1..1000] of integer;
      // Нужна для добавления остатков от деления в массив arr
      count: integer;
   begin
      count:= 0;
     
     repeat
       count:= count + 1;
       arr[count]:= Convert.ToUInt64(num) mod 2;
       num:= Convert.ToUInt64(num) div 2;
     until num = 0;
     
     var resString:= String.Empty;
     
     for var i:= count downto 1 do begin
       resString += arr[i];
     end;
     
     try 
       result:= Convert.ToInt64(resString);
     except exit;
     end;
   end;
   // 2 в степени k
   private static function k2(k: byte): longint;
     begin
       result:= (k > 0) ? 2 * k2(k-1) : 1;
     end;
   /// Перевод двоичного числа в 10 СС
   public static function BinToDec(s: string): longint;
     begin
       if s.Length > 1 then
          BinToDec:=(Ord(s[1])-Ord('0')) * 
           k2(s.Length-1) + BinToDec(Copy(s,2,s.Length-1))
       else BinToDec:=Ord(s[1])-Ord('0');
     end;
   /// Перевод чисел из 10 СС в 16 СС
   public static function DecToHex(num: integer): string;
     var digit: byte;
     var current_char: char;
      begin
        while num > 0 do begin
           digit:= num mod 16;
           if (digit in [10..15])then begin
               case digit of
                   10: current_char:= 'A';
                   11: current_char:= 'B'; 
                   12: current_char:= 'C';
                   13: current_char:= 'D';
                   14: current_char:= 'E';
                   15: current_char:= 'F';
               end;
          end
          else begin
             current_char:= chr(ord('0') + digit);
          end;
         result:= current_char + result;
         num:= num div 16;
        end;
     end;
   /// Перевод чисел из 16 СС в 10 СС
   public static function HexToDec(hex: string): integer;
      begin
         result:= Int32.Parse(hex, NumberStyles.HexNumber);
      end;
   /// Универсальная функция перевода из 10СС в любую сс
   public static function FromDec(num,r:longint): string;
      var s: string;
      const digits: string[16] = '0123456789ABCDEF';
       begin
         s:='';
         repeat
             s:=digits[(num mod r) + 1] + s;
             num:=num div r;
         until num = 0;
        FromDec:= s;
       end;
   /// Универсальная фунция перевода любой сс в 10 СС
   public static function ToDec(num: string; r: longint): longint;
        var m:longint;
        const digits: string[16] = '0123456789ABCDEF';
         begin
            m:=0;
            while num[1] = '0' do delete(num,1,1);
            for var i:=1 to num.Length do
               m:= m * r + pos(num[i],digits)- 1;
           ToDec:=m;
        end;
   /// Перевод чисел из 10 СС в 8 СС
   public static function DecToOct(num: integer): integer;
       begin
          result:= Convert.ToInt32(FromDec(longint(num), 8));
       end;
   /// Перевод чисел из 8 СС в 10 СС
   public static function OctToDec(num: integer): integer;
      begin
         result:= Convert.ToInt32(ToDec(num.ToString(), 8));
      end;
   /// Перевод чисел из 16 СС в 2 СС
   public static function HexToBin(hex: string): double;
     begin
        var decResult:= toDec(hex, 16); 
        result:= DecToBin(decResult);
     end;
   /// Перевод чисел из 2 СС в 16 СС
   public static function BinToHex(num: integer): string;
     begin
        var decResult:= BinToDec(num.ToString());
        result:= DecToHex(decResult);
     end;
end;
/// Обеспечивает взаимодействие кода с панелью систем счисления
type SystemOperationController = static class
   private static function getInfoFromTextBox(textbox_: TextBox): string;
      begin
        result:= textbox_.Text;
      end;
   /// Преобразование 10 -> 2
   private static function getDecToBin(): double;
      begin
        result:= SystemNumerationAPI.DecToBin(Convert.ToInt32(tbSourceNumber.Text));
      end;
   /// Преобразование 10 -> 8
   private static function getDecToOct(): integer;
      begin
        result:= SystemNumerationAPI.DecToOct(Convert.ToInt32(tbSourceNumber.Text));
      end;
   /// Преобразование 10 -> 16
   private static function getDecToHex(): string;
      begin
        result:= SystemNumerationAPI.DecToHex(Convert.ToInt32(tbSourceNumber.Text));
      end;
   /// Преобразование 8 -> 2
   private static function getOctToBin(): double;
      begin
         var dec:= SystemNumerationAPI.OctToDec(Convert.ToInt32(tbSourceNumber.Text));
         result:= SystemNumerationAPI.DecToBin(dec);
      end;
   /// Преобразование 8 -> 10
   private static function getOctToDec(): longint;
      begin
          result:= SystemNumerationAPI.OctToDec(Convert.ToInt32(tbSourceNumber.Text));
      end;
   /// Преобразование 8 -> 16
   private static function getOctToHex(): string;
      begin
          var dec:= SystemNumerationAPI.OctToDec(Convert.ToInt32(tbSourceNumber.Text));
          result:= SystemNumerationAPI.DecToHex(dec);
      end;
   /// Преобразование 2 -> 8
   private static function getBinToOct(): integer;
      begin
        var dec:= SystemNumerationAPI.BinToDec(tbSourceNumber.Text);
        result:= SystemNumerationAPI.DecToOct(integer(dec));
      end;
   /// Преобразование 2 -> 10   
   private static function getBinToDec(): longint;
      begin
         result:= SystemNumerationAPI.BinToDec(tbSourceNumber.Text);
      end;
   /// Преобразование 2 -> 16   
   private static function getBinToHex(): string;
      begin
        result:= SystemNumerationAPI.BinToHex(Convert.ToInt32(tbSourceNumber.Text));
      end;
   /// Преобразование 16 -> 2
   private static function getHexToBin(): double;
      begin
         result:= SystemNumerationAPI.HexToBin(tbSourceNumber.Text);
      end;
   /// Преобразование 16 -> 8
   private static function getHexToOct(): integer;
      begin
         var dec:= SystemNumerationAPI.HexToDec(tbSourceNumber.Text);
         result:= SystemNumerationAPI.DecToOct(dec);
      end;
   /// Преобразование 16 -> 10
   private static function getHexToDec(): integer;
      begin
         result:= SystemNumerationAPI.HexToDec(tbSourceNumber.Text);
      end;
   
   private static procedure getLabelsForSystem(label1_text, label2_text, label3_text: string);
      begin
         system_2_Label.Text:= label1_text;
         system_8_Label.Text:= label2_text;
         system_16_Label.Text:= label3_text;
      end;
   
   public static procedure OnChangeTextBoxSourceNumber(sender: object; args: EventArgs);
      begin
       try
        if (tbSourceNumber.Text.Length > 0)and(cbBase.Text <> nil) then begin
           case cbBase.SelectedIndex of
               0: begin // if 2
                     SystemOperationController.getLabelsForSystem('8 CC','10 CC','16 CC');
                     tb2.Text:= getBinToOct().ToString();
                     tb8.Text:= getBinToDec().ToString();
                     tb16.Text:= getBinToHex();
                  end;
               1: begin // if 8
                     SystemOperationController.getLabelsForSystem('2 CC','10 CC','16 CC');
                     tb2.Text:= getOctToBin().ToString();
                     tb8.Text:= getOctToDec().ToString();
                     tb16.Text:= getOctToHex();
                  end;
               2: begin // if 10
                     SystemOperationController.getLabelsForSystem('2 CC','8 CC','16 CC');
                     tb2.Text:= getDecToBin().ToString();
                     tb8.Text:= getDecToOct().ToString();
                     tb16.Text:= getDecToHex();
                  end;
               3: begin // if 16
                     SystemOperationController.getLabelsForSystem('2 CC','8 CC','10 CC');
                     tb2.Text:= getHexToBin().ToString();
                     tb8.Text:= getHexToOct().ToString();
                     tb16.Text:= getHexToDec().ToString();
                  end;
           end;
        end;
        except MessageBox.Show('Строка имела неверный для данного типа числа формат', 'Error', MessageBoxButtons.OK,
          MessageBoxIcon.Error);
        end;
      end;
end;
/// Обеспечивает взаимодействие кода с панелью битовых операторов
type BitwiseOperationController = static class
     /// Возвращает первый операнд
     private static function getFirstNumber(): string;
        begin
           result:= tbFirst.Text;
        end;
     /// Возвращает второй операнд
     private static function getSecondNumber(): string;
        begin
           result:= tbSecond.Text;
        end;
     /// Реализация гейта NOT
     private static function GateNOT(): string;
         begin
            var binaryString:= Convert.ToInt32(getFirstNumber()).ToString();; // bin 
            var resultString:= String.Empty;
            
            for var i:= 1 to binaryString.Length do begin
               if (binaryString[i] = '1')then resultString += '0'
               else resultString += '1';
            end;
            
            result:= resultString;
         end;
     /// Содержит ли строка в себе только двоичные цифры
     private static function IsBinaryDigit(numberString: string): boolean;
       begin
        var binaryDigits: string:='10';
       
         for var i:= 1 to numberString.Length do begin
           if (binaryDigits.IndexOf(numberString[i]) = -1)then begin
              result:= false;
              exit;
           end;
         end;
        result:= true;
       end;

     public static procedure OnChangeOperation(sender: object; args: EventArgs);
      var first, second: longint;
        begin
          if (cbBitwise.Text <> String.Empty) then begin
             // Пусты ли строки для числовых значений
             var operandsIsNullOrEmpty: boolean:= String.IsNullOrWhiteSpace(getFirstNumber()) or (String.IsNullOrWhiteSpace(getSecondNumber()));
             
             var operandsIsBinDigit: boolean:= BitwiseOperationController.IsBinaryDigit(getFirstNumber);
                                          
              if (operandsIsNullOrEmpty)then begin 
                 MessageBox.Show('Строки пусты', 'Error', MessageBoxButtons.OK, MessageBoxIcon.Error);
              end;
              
             if(operandsIsBinDigit)then begin
       
               first:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getFirstNumber());
               case cbBitwise.SelectedIndex of
                 0: begin 
                       second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                       tbResult.Text:= SystemNumerationAPI.DecToBin(first and second).ToString();
                   end;
                 1: begin
                       second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                       tbResult.Text:= SystemNumerationAPI.DecToBin(first or second).ToString();
                    end;   
                 2: begin 
                       second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                       tbResult.Text:= SystemNumerationAPI.DecToBin(first xor second).ToString();
                   end;    
                 3: tbResult.Text:= BitwiseOperationController.GateNOT();
                 // IMPORTANT! В операциях shl и shr 2-й параметр должен быть десятичным числом(dec)
                 4: begin
                        second:= Convert.ToInt64(BitwiseOperationController.getSecondNumber());
                        tbResult.Text:= SystemNumerationAPI.DecToBin(first shl second).ToString();
                    end;
                 5: begin
                        second:= Convert.ToInt64(BitwiseOperationController.getSecondNumber());
                        tbResult.Text:= SystemNumerationAPI.DecToBin(first shr second).ToString();
                    end;
                 6: begin
                       second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                       var sum:= first + second;
                       tbResult.Text:= SystemNumerationAPI.DecToBin(sum).ToString();
                    end;
                 7: begin
                      second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                      var sub:= first - second;
                      tbResult.Text:= SystemNumerationAPI.DecToBin(sub).ToString();
                    end;
                 8: begin
                      second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                      var mul:= first * second;
                      tbResult.Text:= SystemNumerationAPI.DecToBin(mul).ToString();
                    end;
                 9: begin
                      second:= SystemNumerationAPI.BinToDec(BitwiseOperationController.getSecondNumber());
                      var divide:= first / second;
                      // IMPORTANT: При делении числа real здесь округляются до int32
                      tbResult.Text:= '~' + SystemNumerationAPI.DecToBin(divide.Round()).ToString();
                    end;
                end;
               end
               else raise new Exception('Числа не являются двоичными');
            end 
            else exit;
        end;
end;

/// Отвечает за инициализацию приложения
type BitwiseInitializer = sealed class
      public constructor Create();
       begin
         if (&File.Exists(BACKGROUND_CONSTANT))then Initialize()
         else begin
            MessageBox.Show('Error: File ' + BACKGROUND_CONSTANT + ' don"t exists!', 'Error',
                MessageBoxButtons.OK, MessageBoxIcon.Error);
         end;
       end;
       
     public procedure Initialize();
       begin
          f.BackgroundImage := Image.FromFile(BACKGROUND_CONSTANT);
          f.ClientSize := new Size(623, 223);      
          f.Text := 'Bitwise V' + VERSION_APP;

          tbSourceNumber.BackColor := BLACK_COLOR;
          tbSourceNumber.BorderStyle := BorderStyle.FixedSingle;
          tbSourceNumber.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
          tbSourceNumber.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
          tbSourceNumber.Location := new Point(5, 36);
          tbSourceNumber.Size := new Size(151, 30);
          // sourceNumberLabel
          sourceNumberLabel.BackColor := TRSP_COLOR;
          sourceNumberLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
          sourceNumberLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
          sourceNumberLabel.Location := new Point(5, 7);
          sourceNumberLabel.Size := new Size(141, 22);
          sourceNumberLabel.Text := 'Исходное число:';
          // cbBase
         cbBase.BackColor := BLACK_COLOR;
         cbBase.FlatStyle := FlatStyle.Popup;
         cbBase.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         cbBase.ForeColor := Color.FromArgb((System.Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         cbBase.FormattingEnabled := true;
         cbBase.Items.AddRange(new Object[4]('2 СС', '8 СС', '10 СС', '16 СС'));
         cbBase.Location := new Point(162, 36);
         cbBase.Size := new Size(68, 31);
         cbBase.TextChanged += SystemOperationController.OnChangeTextBoxSourceNumber;
         // system_2_Label
         system_2_Label.BackColor := TRSP_COLOR;
         system_2_Label.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         system_2_Label.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         system_2_Label.Location := new Point(5, 78);
         system_2_Label.Size := new Size(54, 26);
         system_2_Label.Text := '2 СС:';
         // tb2
         tb2.BackColor := BLACK_COLOR;
         tb2.BorderStyle := BorderStyle.FixedSingle;
         tb2.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         tb2.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         tb2.Location := new Point(68, 74);
         tb2.Size := new Size(151, 30);
         // system_8_Label
         system_8_Label.BackColor := TRSP_COLOR;
         system_8_Label.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         system_8_Label.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         system_8_Label.Location := new Point(5, 123);
         system_8_Label.Size := new Size(54, 26);
         system_8_Label.Text := '8 СС:';
         // system_16_Label
         system_16_Label.BackColor := TRSP_COLOR;
         system_16_Label.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         system_16_Label.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         system_16_Label.Location := new Point(5, 163);
         system_16_Label.Size := new Size(62, 26);
         system_16_Label.Text := '16 СС:';
         // tb8
         tb8.BackColor := BLACK_COLOR;
         tb8.BorderStyle := BorderStyle.FixedSingle;
         tb8.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
         tb8.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
         tb8.Location := new Point(68, 119);
         tb8.Size := new Size(151, 30);
         // tb16
        tb16.BackColor := BLACK_COLOR;
        tb16.BorderStyle := BorderStyle.FixedSingle;
        tb16.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        tb16.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        tb16.Location := new Point(68, 159);
        tb16.Size := new Size(151, 30);
        // baseLabel
        baseLabel.BackColor := TRSP_COLOR;
        baseLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        baseLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        baseLabel.Location := new Point(147, 7);
        baseLabel.Size := new Size(98, 22);
        baseLabel.Text := 'Основание:';
        // panel_left
        panel_left.BackgroundImage := Image.FromFile(BACKGROUND_CONSTANT);
        panel_left.BorderStyle := BorderStyle.FixedSingle;
        panel_left.Controls.Add(tb2);
        panel_left.Controls.Add(baseLabel);
        panel_left.Controls.Add(tbSourceNumber);
        panel_left.Controls.Add(tb16);
        panel_left.Controls.Add(sourceNumberLabel);
        panel_left.Controls.Add(tb8);
        panel_left.Controls.Add(cbBase);
        panel_left.Controls.Add(system_16_Label);
        panel_left.Controls.Add(system_2_Label);
        panel_left.Controls.Add(system_8_Label);
        panel_left.Location := new Point(12, 12);
        panel_left.Name := 'panel_left';
        panel_left.Size := new Size(248, 200);
        panel_left.TabIndex := 11;
        // panel_right
        panel_right.BackgroundImage := Image.FromFile(BACKGROUND_CONSTANT);
        panel_right.BorderStyle := BorderStyle.FixedSingle;
        panel_right.Controls.Add(tbResult);
        panel_right.Controls.Add(ResultLabel);
        panel_right.Controls.Add(cbBitwise);
        panel_right.Controls.Add(OperationLabel);
        panel_right.Controls.Add(tbSecond);
        panel_right.Controls.Add(tbFirst);
        panel_right.Controls.Add(SecondNumLabel);
        panel_right.Controls.Add(FirstNumLabel);
        panel_right.Controls.Add(bitwiseLabel);
        panel_right.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        panel_right.Location := new Point(266, 12);
        panel_right.Size := new Size(350, 200);
        // bitwiseLabel
        bitwiseLabel.BackColor := TRSP_COLOR;
        bitwiseLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        bitwiseLabel.Location := new Point(86, 0);
        bitwiseLabel.Size := new Size(168, 30);
        bitwiseLabel.Text := 'Битовые операции:';
        // FirstNumLabel
        FirstNumLabel.BackColor := TRSP_COLOR;
        FirstNumLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        FirstNumLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        FirstNumLabel.Location := new Point(3, 36);
        FirstNumLabel.Size := new Size(80, 26);
        FirstNumLabel.Text := '1 число:';
        // SecondNumLabel
        SecondNumLabel.BackColor := TRSP_COLOR;
        SecondNumLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        SecondNumLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        SecondNumLabel.Location := new Point(3, 78);
        SecondNumLabel.Size := new Size(80, 26);
        SecondNumLabel.Text := '2 число:';
        // tbFirst
        tbFirst.BackColor := BLACK_COLOR;
        tbFirst.BorderStyle := BorderStyle.FixedSingle;
        tbFirst.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        tbFirst.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        tbFirst.Location := new Point(89, 37);
        tbFirst.Size := new Size(255, 30);
        // tbSecond
        tbSecond.BackColor := BLACK_COLOR;
        tbSecond.BorderStyle := BorderStyle.FixedSingle;
        tbSecond.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        tbSecond.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        tbSecond.Location := new Point(89, 73);
        tbSecond.Size := new Size(255, 30);
        // OperationLabel
        OperationLabel.BackColor := TRSP_COLOR;
        OperationLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        OperationLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        OperationLabel.Location := new Point(3, 119);
        OperationLabel.Size := new Size(93, 26);
        OperationLabel.Text := 'Операция:';
        // cbBitwise
        cbBitwise.BackColor := BLACK_COLOR;
        cbBitwise.FlatStyle := FlatStyle.Popup;
        cbBitwise.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        cbBitwise.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        cbBitwise.FormattingEnabled := true;
        cbBitwise.Items.AddRange(new Object[10]('&(and)', '|(or)', '^(xor)', '~(not)', '<<(shl)', '>>(shr)', '+(add)', '-(sub)', '*(mul)', '/(div)'));
        cbBitwise.Location := new Point(102, 114);
        cbBitwise.Size := new Size(74, 31);
        cbBitwise.TextChanged += BitwiseOperationController.OnChangeOperation;
        // ResultLabel
        ResultLabel.BackColor := TRSP_COLOR;
        ResultLabel.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        ResultLabel.ForeColor := Color.FromArgb((Int32((Byte(192)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        ResultLabel.Location := new Point(3, 159);
        ResultLabel.Size := new Size(93, 26);
        ResultLabel.Text := 'Результат:';
        // tbResult
        tbResult.BackColor := BLACK_COLOR;
        tbResult.BorderStyle := BorderStyle.FixedSingle;
        tbResult.Font := new Font('Comic Sans MS', 12, FontStyle.Regular, GraphicsUnit.Point, (Byte(204)));
        tbResult.ForeColor := Color.FromArgb((Int32((Byte(255)))), (Int32((Byte(255)))), (Int32((Byte(192)))));
        tbResult.Location := new Point(89, 156);
        tbResult.Size := new Size(255, 30);
 
        f.Controls.Add(panel_right);
        f.Controls.Add(panel_left);
       end;
     /// Запускает приложение
     public procedure Run();
        begin
          Application.Run(f);
        end;
end;

begin
 var bitwiseCalc:= new BitwiseInitializer();
 bitwiseCalc.Run();
end.
