//-------------- Стек -------------//
type
  //Указатель на элемент стека.
  TPElem = ^TElem;
  //Элемент стека.
  TElem = record
    Data : Integer;
    PNext : TPElem;
  end;
 
//Добавление элемента на вершину стека.
procedure StackPush(var aPStack, aPElem : TPElem);
begin
  if aPElem = nil then Exit;
  aPElem^.PNext := aPStack;
  aPStack := aPElem
end;
 
//Изъятие элемента с вершины стека.
function StackPop(var aPStack, aPElem : TPElem) : Boolean;
begin
  Result := False;
  if aPStack = nil then Exit;
  aPElem := aPStack;
  aPStack := aPElem^.PNext;
  Result := True;
end;
 
//Удаление стека из памяти (очистка стека).
procedure StackFree(var aPStack : TPElem);
var
  PDel : TPElem;
begin
  while aPStack <> nil do begin
    PDel := aPStack;
    aPStack := aPStack^.PNext;
    Dispose(PDel);
  end;
end;
 
const M = 10;
var PSt1, PSt2, PSt3, PElem : TPElem;
  i, NumMax,NumMin : Integer;
  S : String;
begin
  repeat
    PSt1 := nil;
    PSt2 := nil;
 
    //Формируем содержимое первого стека.
    Randomize;
    for i := 1 to M do begin
      New(PElem);
      if i = (M div 2) then
        PElem^.Data := 100
      else
        PElem^.Data := i
      ;
      StackPush(PSt1, PElem);
    end;
 
    //Распечатка и переливание из первого стека во второй стек.
    //Одновременно определяем наибольший элемент.
    Writeln('Содержимое первого стека:');
    i := 0;
    NumMax := 0;
    while StackPop(PSt1, PElem) do begin
      Inc(i);
      if i = 0 then
        NumMax := PElem^.Data
      else begin
        if PElem^.Data > NumMax then NumMax := PElem^.Data;
      end;
      StackPush(PSt2, PElem);
      //Распечатка.
      if i > 1 then Write(', ');
      Write(PElem^.Data);
    end;
    Writeln;
    Writeln('Максимальное значение: ', NumMax);
 
    //Теперь переливаем элементы из второго стека обратно - в первый стек.
    while StackPop(PSt2, PElem) do StackPush(PSt1, PElem);
    //Переливаем из первого стека во второй только те элементы,
    //которые не равны максимальному.
    while StackPop(PSt1, PElem) do begin
      if PElem^.Data <> NumMax then StackPush(PSt2, PElem);
    end;
       //Формируем содержимое первого стека.
    Randomize;
    for i := 1 to M do begin
      New(PElem);
      if i = (M div 2) then
        PElem^.Data := 1
      else
        PElem^.Data := i
      ;
      StackPush(PSt2, PElem);
    end;
    //Распечатка элементов второго стека.
    //При этом, стек очищается.
    Writeln('Содержимое второго стека:');
    i := 0;
    NumMin := 0;
    while StackPop(PSt2, PElem) do begin
      Inc(i);
      if i = 0 then
      NumMin := PElem^.Data
      else begin
        if PElem^.Data < NumMin then NumMin := PElem^.Data;
      end;
      StackPush(PSt3, PElem);
      //Распечатка.
      if i > 0 then Write(', ');
      Write(PElem^.Data);
    end;
    Writeln;
     Writeln;
    Writeln('Минимальное значение: ', NumMin);
   
    while StackPop(PSt3, PElem) do StackPush(PSt2, PElem);
    
    while StackPop(PSt2, PElem) do begin
      if PElem^.Data <> NumMin then StackPush(PSt3, PElem);
    end;

    StackFree(PSt1);
    StackFree(PSt2);
    Writeln('Работа завершена. Стеки удалены из памяти.');
    Writeln('Повторить - Enter. Выход - любой символ + Enter.');
    Readln(S);
  until S <> '';
end.
//---------- Очередь -------------//
uses crt;
type Queue = ^Node;
     Node = record
       data: integer;
       next: Queue;
     end;
///Добавляет элемент в очередь
procedure AddToQueue(var q: Queue;x: integer);
var
  tmp:Queue;
begin
  New(tmp);
  tmp^.next:=q;
  tmp^.data:=x;
  q:=tmp;
end;
///Выводит все элементы очереди
procedure Print(q: Queue);
begin
  if q=nil then
  begin
    writeln('Очередь пуста.');
    exit;
  end;
  while q<>nil do
  begin 
    Write(q^.data, ' ');
    q:=q^.next
  end;
end;
///Освобождение памяти
procedure FreeQueue(q: Queue);
 var tmp: Queue;
  begin
    while q <> nil do begin
      tmp:= q;
      q:= q^.next;
      Dispose(tmp);
    end;
  end;
///Поиск элемента в очереди по значению
function SearchInQueue(q: Queue; x: integer): Queue;
  begin
    if q<>nil then
     while (q<>nil)and(x<>q^.data)do begin
      q:= q^.next;
      end;
      SearchInQueue:= q;
  end;

///Удаляет элемент по указателю
procedure Delete_Elem(q: Queue; tmp: Queue);
 var tmpi: Queue;
  begin
    if (q = nil) or (tmp = nil)then exit;
    if tmp = q then begin
      q:= tmp^.next;
      Dispose(tmp);
    end
    else begin
      tmpi:= q;
      while tmpi^.next<> tmp do tmpi:=tmpi^.next;
      tmpi^.next:= tmp^.next;     
      Dispose(tmp);
    end;
  end;
//Процедура удаления элемента по значению
procedure DelElemZnach(var q: Queue; x: integer);
 var tmp: Queue;
  begin
   if q = nil then begin
     writeln('Queue is empty');
     exit;
   end;
  tmp:= SearchInQueue(q,x);
if tmp = nil then begin
  writeln('Elem with finded number',' ' ,'not find');
  exit;
end;
 Delete_Elem(q, tmp);
 writeln('Element deleted');
  end;
//Удаление элемента по порядковому номеру (хвост имеет номер 1)
 procedure DelElemPos(q: Queue;position : integer);
  var i: integer;
      tmp: Queue;
begin
  if position < 1 then exit;
  if q = nil then begin
     writeln('Queue is empty');
     exit;
  end;
  i:= 1;
  tmp:= q;
  while (tmp <> nil)and(i<>position)do begin
    tmp:= tmp^.next;
    inc(i);
  end;
  if tmp = nil then begin
     Writeln('Элемента с порядковым номером ' ,position, ' нет в очереди.');
    writeln('В очереди ' ,i-1, ' элементов(а).');
    exit
  end;
 Delete_Elem(q, tmp);
writeln('Element was deleted');
end;
///Процедура сортировки "пузырьком" с изменением только данных
procedure SortBublInf(nach: Queue);
var
  tmp,rab: Queue;
  inf: integer;
begin
  New(tmp); {выделяем память для рабочего "буфера" обмена}
  rab:=nach; {рабочая ссылка, становимся на хвост очереди}
  while rab<>nil do {пока мы не дойдём до конца стека делать}
  begin
    tmp:=rab^.next; {перейдём на следующий элемент}
    while tmp<>nil do {пока не конец очереди делать}
    begin
      if tmp^.data<rab^.data then {проверяем следует ли менять элементы}
      begin
        inf:=tmp^.data; {стандартная замена в 3 операции}
        tmp^.data:=rab^.data;
        rab^.data:=inf
      end;
      tmp:=tmp^.next {переход к следующему элементу}
    end;
    rab:=rab^.next {переход к следующему элементу}
  end
end;
///Процедура сортировки "пузырьком" с изменением только адресов
procedure SortBublLink(nach: Queue);
var
  tmp,pered,pered1,pocle,rab: Queue;
begin
  rab:=nach;
  while rab<>nil do
  begin
    tmp:=rab^.next;
    while tmp<>nil do
    begin
      if tmp^.data<rab^.data then
      begin
        pered:=nach;
        pered1:=nach;
        if rab<>nach then
          while pered^.next<>rab do pered:=pered^.next;
        while pered1^.next<>tmp do pered1:=pered1^.next;
        pocle:=tmp^.next;
        if rab^.next=tmp then
        begin
          tmp^.next:=rab;
          rab^.next:=pocle
        end
        else
        begin
          tmp^.next:=rab^.next;
          rab^.next:=pocle;
        end;
        if pered1<>rab then
          pered1^.next:=rab;
        if rab<>nach then
          pered^.next:=tmp
        else
          nach:=tmp;
        pered1:=tmp;
        tmp:=rab;
        rab:=pered1;
      end;
      tmp:=tmp^.next; 
    end;
    rab:=rab^.next;
  end;
end;
var s,tmp: Queue;
    zn: integer;
    ch: char;
begin
 S:=nil;
  repeat {цикл для нашего меню}
    clrscr; {очистка экрана, далее идёт вывод самого меню}
    Writeln('Программа для работы с очередями.');
    Writeln('Выберите желаемое действие:');
    Writeln('1) Добавить элемент.');
    Writeln('2) Вывод очереди (начиная с головы).');
    Writeln('3) Удаление элемента по значению.');
    Writeln('4) Удаление элемента по порядковому номеру.');
    Writeln('5) Поиск элемента по значению.');
    Writeln('6) Сортировка очереди методом "Пузырька", меняя только данные.');
    Writeln('7) Сортировка очереди методом "Пузырька", меняя только адрес.');
    Writeln('8) Выход.');
    writeln;
    ch:=readkey;
    case ch of
      '1':begin
            write('Введите значение добавляемого элемента: ');
            readln(zn);
            AddToQueue(S,zn);
          end;
      '2':begin
            clrscr;
            Print(S);
            readkey;
          end;
      '3':begin
            Write('Введите значение удаляемого элемента: ');
            readln(zn);
            DelElemZnach(S,zn);
            readkey;
          end;
      '4':begin
            Write('Введите порядковый номер удаляемого элемента: ');
            readln(zn);
            DelElemPos(S,zn);
            readkey;
          end;
      '5':begin
            write('Введите значение искомого элемента: ');
            readln(zn);
            tmp:=SearchInQueue(S,zn);
            if tmp=nil then
              write('Искомый элемент отсутствует в очереди.')
            else
              write('Элемент ',tmp^.data,' найден.');
            readkey;
          end;
      '6':begin
            if S=nil then
            begin
              Write('Очередь пуста.');
              readkey
            end
            else
            begin
              SortBublInf(S);
              Write('Очередь отсортирована.');
              readkey;
            end
          end;
      '7':begin
            if S=nil then
            begin
              Write('Очередь пуста.');
              readkey
            end
            else
            begin
              SortBublLink(S);
              Write('Очередь отсортирована.');
              readkey;
            end
          end;
    end;
  until ch='8';
  FreeQueue(S);
end.
//----------- Деревья --------------//
label link;
type PNode = ^Node;
     Node = record
        data: integer;
        left, right: PNode;
     end;
///Функция поиска по дереву
function Search(Tree: PNode; x: integer): PNode;
  begin
    if Tree = nil then begin
       result:= nil;
       exit;
    end;
    if x= Tree^.data then
      result:= Tree
    else if x < Tree^.data then
        result:= Search(Tree^.left, x)
    else result:= Search(Tree^.right, x);
  end;
 ///Процедура добавления элемента в дерево
 procedure AddToTree(var Tree: PNode; x: integer);
    begin
      if Tree = nil then begin
        New(Tree);
        Tree^.data:= x;
        Tree^.left:= nil;
        Tree^.right:= nil;
        exit;
      end;
      {Добавляем к левому или правому поддереву это зависит от вводимого элемента}
      if x < Tree^.data then AddToTree(Tree^.left, x)
      else AddToTree(Tree^.right,x);
    end;
 ///Обход дерева по принципу Левый-корень-правый 
  procedure LKPObhod(tree: PNode);
    begin
      if Tree = nil then Exit;
      LKPObhod(Tree^.left);
      write(' ',Tree^.data);
      LKPObhod(Tree^.right);
    end;
 ///Удаляет элементы из дерева
  procedure DeleteTree( var Tree: PNode);
    begin
      if Tree <> nil then begin
        DeleteTree(Tree^.left);
        DeleteTree(Tree^.right);
        Dispose(Tree);
      end;
    end;
 var Tree, p: PNode;
  begin
   Tree:= nil;
   var n,x: integer;
   var answer: char;
   while true do begin
     writeln;
     writeln('Continue next? Y or N');
     readln(answer);
     if answer = 'N' then goto link;
     
     Writeln('Enter command: ');
     writeln('--------------------------');
     writeln('AddToTree - add');
     writeln('Search -srh');
     writeln('LKPObhod - lkp');
     writeln('DeleteTree - del');
     writeln('--------------------------');
 var command: string;
     readln(command);
     case command of 
         'add': begin
                  writeln('Enter the number of numbers to enter');
                  readln(n);
                  for var i:=1 to n do begin
                        readln(x);
                        AddToTree(Tree,x);
                  end;
                end;
         'srh': begin
                   writeln('Enter an item to search');
                   readln(n);
                   p:= Search(Tree, n);
                   if p <> nil then writeln('Found')
                   else writeln('Not Found');
                end;
         'lkp': begin
                  writeln('The output of the tree:');
                  LKPObhod(tree);
                end;
         'del': begin
                 DeleteTree(Tree);
                 writeln('Tree deleted');
                end;
     end;
   end;
  link: halt;
 end.
//----------------------------------------//
////Создать массив используя список и вывести элементы массива
type F = ^tp;
     tp = record
		i: integer;
		next: F;
	 end;
var a: array[1..10]of F;
	 lastA, j: integer;
	 item: F;
// Процедура добавляющая в хвост массива новый элемент и устанавливающая на него хвост
 function AddA:F;
  begin
	lastA:=lastA+1;
	new(a[lastA]);
	AddA:=a[lastA];
  end;
 begin
	lastA:=0;
	item:=AddA;
	item^.i:=10;
	item:=AddA;
	item^.i:=20;
	item:=AddA;
	item^.i:=30;
	for j:=1 to 10 do begin
	  if a[j]<> nil then
	     write(a[j]^.i:4);
	end;
 end.
//-------------------------------------------------------//
Создать односвязный список и вывести его элементы

type PNode = ^TNode;
	 TNode = record
		data: integer;
		next: PNode;
	 end;
function NewNode(d: integer; n: PNode): PNode;
 begin
	New(Result);
	Result^.data:= d;
	Result^.next:= n;
 end;
 var first: PNode;
 begin
	first:= nil;
	//Добавляем в начало односвязного списка
	first:= NewNode(3,first);
	first:= NewNode(7,first);
	first:=NewNode(5,first);
	//Вывод односвязного списка
	var p:=first;
	while p<>nil do
		begin
			write(p^.data,' ');
			p:=p^.next;
		end;
	//Разрушение односвязного списка
	p:=first;
	while p<>nil do begin
		var p1:=p;
		p:=p^.next;
		Dispose(p1);
	end;
 end.
//------------------------------------------//
Использование ссылок вместо указателей для создания односвязного списка

type 
  Node = class
    data: integer;
    next: Node;
    constructor (d: integer; n: Node);
    begin
      data := d;
      next := n;
    end;
  end;

var first: Node;
  
begin
  first := nil;
  first := new Node(3,first);
  first := new Node(7,first);
  first := new Node(5,first);
  
  writeln('Содержимое односвязного списка (использование ссылок вместо указателей): ');
  var p := first;
  while p<>nil do
  begin
    write(p.data,' ');
    p := p.next;
  end;
  
  first := nil;
end.
//---------------------
// Pointer3
type PNode = ^TNode;
	TNode = record
		data:integer;
		next:PNode;
	end;
var p1,PAdd: PNode;
	D: integer;
begin
 write('D= ');
 readln(D);
 
 New(p1);
 New(PAdd);
 PAdd^.data:=D;
 p1^.Next:=PAdd;
 writeln(pAdd^.data);
 writeln(pAdd^.next);
end.
//---------------------
// Pointer4
type PNode = ^TNode;
   TNode = record
      data: integer;
      next: PNode;
   end;
var N,d: integer;
    link,p: PNode;
procedure AddElem(p: PNode);
 begin
   write('N= ');
   readln(N);
   p:=nil;
   for var i:=1 to N do begin
     new(link);
     readln(link^.data);
     link^.next:=p;
   end;
 end;
 begin
 AddElem(p);
 end.
//---------------------
// Pointer5
type PNode = ^TNode;
     TNode = record
      data: integer;
      next: PNode;
     end;
var p,p1,p2: PNode;
   N,D: integer;
procedure CreateStack(p1: PNode);
 begin
  readln(N);
  p1:= nil;
  for var i:=1 to N do begin
    new(p);
    readln(p^.data);
    p^.next:=p1;
    p1:=p;
  end;
 end;
begin
 CreateStack(p1);
 D:=p^.data;
 if p^.next <> nil then p2:=p^.next
 else p2:=nil;
 Dispose(p);
 writeln('Data=',D);
 writeln(p2)
end.
//---------------------
// Pointer6
type
  PNode=^TNode;
  TNode=record
    Data: Integer;
    Next: PNode;
  end;
 var N:integer;
procedure CreateStack(var P1:PNode);
var
 p:PNode;
 i: integer;
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
function pop(var P1:PNode):integer;
var
 head: PNode;
begin
 Result:=p1^.Data;
 head:=P1^.Next;
 Dispose(P1);
 P1:=head;
end;
var
 P1:PNode;
 i:integer;
begin
 CreateStack(P1);
 for i:=1 to N do Writeln(pop(P1));
end.
//---------------------
// Pointer7
type PNode = ^TNode;
      TNode = record
         data: integer;
         next: PNode;
      end;
 var p1,p:PNode;
      N: integer;
procedure CreateStack(var P1:PNode);
var
 p:PNode;
 i,N:integer;
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
function pop(var P1:PNode):integer;
var
 head: PNode;
begin
 Result:=p1^.Data;
 head:=P1^.Next;
 Dispose(P1);
 P1:=head;
end;
begin
  CreateStack(P1);
 N:=0;
 while (p1<>nil) do
  begin
   inc(N);
   Writeln(pop(P1));
  end;
 Writeln('len:',N);
end.
//---------------------
// Pointer8;
type PNode = ^TNode;
     TNode = record
        data: integer;
        next: PNode;
     end;
procedure CreateStack(var P1:PNode);
var
 p:PNode;
 i,N:integer;
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
 var pt1: PNode;
 procedure Cat(p1,p2: PNode);
  begin
   pt1:=p1;
   while (pt1^.next <> nil)do pt1:=pt1^.next;
   pt1^.next:=p2;
  end;
 var p1,p2: PNode;
 begin
  CreateStack(p1);
  CreateStack(p2);
  Cat(p1,p2);
 end.
//---------------------
// Pointer9
type
  PNode=^TNode;
  TNode=record
    Data: Integer;
    Next: PNode;
  end;
procedure CreateStack(var P1:PNode);
var p:PNode;
 i,N:integer;
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
procedure cat2(var P1:PNode; P2:PNode);
var
 Pt1:PNode;
begin
 Pt1:=P1;
 while (Pt1^.Next<>nil) do Pt1:=Pt1^.Next;
 Pt1^.Next:=P2;
 while (Pt1^.Next<>nil) do
  begin
   pt1:= pt1^.Next;
   if (Pt1^.Next^.Data mod 2)=0 then
     pt1^.Next:= nil;
  end;
end;
var P1,P2:PNode;
begin
 CreateStack(P1);
 CreateStack(P2);
 Cat2(P2,P1);
end.
//---------------------
// Pointer10
type
  PNode=^TNode;
  TNode=record
    Data: Integer;
    Next: PNode;
//    Prev: PNode;
  end;
 
procedure CreateStack(var P1:PNode);
var
 p:PNode;
 i,N:integer;
 
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
 
procedure split(P1:PNode; var P2,P3:PNode);
var
 Pt2,Pt3,Temp:PNode;
begin
 Pt2:=nil;
 Pt3:=nil;
 P2:=nil;
 P3:=nil;
 
 temp:=p1;
 writeln(temp^.Data);
 while (temp<>nil) do
  begin
 
   if (temp^.Data mod 2) = 0 then
    begin
     if p2= nil then
      begin
       p2:=temp;
       Pt2:=temp;
      end
     else
      begin
       Pt2^.Next:=temp;
       Pt2:=Pt2^.Next;
      end;
    end
   else
    begin
     if p3 = nil then
      begin
       p3:=temp;
       Pt3:=temp;
      end
     else
      begin
       Pt3^.Next:=temp;
       Pt3:=Pt3^.Next;
      end;
    end;
   if temp^.Next = nil then temp:= nil
   else temp:= temp^.Next;
  end;
  Pt2^.Next:=nil;
  Pt3^.Next:=nil;
end;
 
var
 P1,P2,P3:PNode;
begin
 CreateStack(P1);
 Split(P1,P2,P3);
end.
//---------------------
// Pointer11
program Pointer11;
type
  PNode=^TNode;
  TNode=record
    Data: Integer;
    Next: PNode;
  end;
  TStack=record
    Top:PNode;
  end;
 
procedure CreateStack(var P1:PNode);
var
 p:PNode;
 i,N:integer;
 
begin
 Write('N:');
 Readln(N);
 p1:=nil;
 for i:=1 to N do
  begin
   new(p);
   Write('num: ');
   Readln(P^.Data);
   p^.Next := P1;
   P1:=p;
  end;
end;
 
Procedure Push(var S:TStack;D:Integer);
var
 temp:PNode;
begin
 new(temp);
 temp^.Data:=D;
 temp^.Next:=S.Top;
 S.Top:=temp;
end;
 
var
 S:TStack;
 N,D,i:integer;
begin
 CreateStack(S.Top);
 Writeln('N:');
 Readln(N);
 For i:=1 to N do
  begin
   Writeln('num:');
   Readln(D);
   Push(S,D);
  end;
end.
//------------- Односвязный список со всеми функциями ---------//
type TData = Integer;
  TPElem = ^TElem;
  TElem = record
    Data : TData; 
    PNext : TPElem; 
  end;

  TDList = record
    PFirst, PLast : TPElem;
  end;
 
{IMPORTANT! Процедуру вызывать только для инициализации пустого списка, иначе
будет утечка памяти.}
procedure Init(var aL : TDList);
begin
  aL.PFirst := nil;
  aL.PLast := nil;
end;
 
{Добавление элемента в конец списка.}
procedure Add(var aL : TDList; const aData : TData);
var
  PElem : TPElem;
begin
  New(PElem);
  PElem^.Data := aData;
  PElem^.PNext := nil;
 
  if aL.PFirst = nil then
    aL.PFirst := PElem
  else
    aL.PLast^.PNext := PElem;
  aL.PLast := PElem;
end;
 
{Освобождение памяти}
procedure ListFree(var aL : TDList);
var
  PElem, PDel : TPElem;
begin
  PElem := aL.PFirst;
  while PElem <> nil do
  begin
    PDel := PElem;
    PElem := PElem^.PNext;
    Dispose(PDel);
  end;
  Init(aL);
end;
 
procedure LWriteln(var aL : TDList);
var
  PElem : TPElem;
begin
  PElem := aL.PFirst;
  if PElem = nil then
    Write('Список пуст.')
  else
    while PElem <> nil do
    begin
      if PElem <> aL.PFirst then
        Write(', ');
      Write(PElem^.Data);
      PElem := PElem^.PNext;
    end;
  Writeln;
end;
 
{Удаление из списка элементов с заданным значением.}
function DelByVal(var aL : TDList; const aData : TData) : Integer;
var
  PElem, PPrev, PDel : TPElem;
  Cnt : Integer;
begin
  Cnt := 0; {Счётчик удалённых элементов.}
  PPrev := nil; {Указатель на предыдущий элемент.}
  PElem := aL.PFirst; {Указатель на текущий элемент.}
  while PElem <> nil do
    if PElem^.Data = aData then
    begin
      {Если удаляемый элемент является первым элементом списка, то первым
      элементом списка назначаем следующий элемент.}
      if PElem = aL.PFirst then
        aL.PFirst := PElem^.PNext
      else
        PPrev^.PNext := PElem^.PNext;
      if PElem = aL.PLast then
        aL.PLast := PPrev;
      PDel := PElem;
      PElem := PElem^.PNext;
      Dispose(PDel);
      Inc(Cnt);
    end
    else
    begin
      PPrev := PElem;
      PElem := PElem^.PNext;
    end;
  DelByVal := Cnt;
end;
 
{Диалог добавления элементов в список.}
procedure WorkAdd(var aL : TDList);
var
  S : String;
  PElem : TPElem;
  Data : TData;
  Code, Cnt : Integer;
begin
  Cnt := 0; // кол-во элементов в списке
  PElem := aL.PFirst;
  while PElem <> nil do
  begin
    Inc(Cnt);
    PElem := PElem^.PNext;
  end;
 
  Writeln('Добавление элементов в список.');
  Writeln('Ввод каждого значения завершайте нажатием Enter.');
  Writeln('Чтобы прекратить ввод, оставьте пустую строку и нажмите Enter.');
  repeat
    Write('Элемент №', Cnt + 1, ': ');
    Readln(S);
    if S <> '' then
    begin
      Val(S, Data, Code);
      if Code = 0 then
      begin
        Add(aL, Data);
        Inc(Cnt);
      end
      else
        Writeln('Неверный ввод. Повторите.');
    end;
  until S = '';
  Writeln('Ввод элементов списка завершён.');
end;
 
var
  L : TDList;
  Data : TData;
  Cmd, Code, Cnt : Integer;
  S : String;
begin
  {Начальная инициализация списка.}
  Init(L);
 
  repeat
    {Меню.}
    Writeln('Выберите действие:');
    Writeln('1: Добавление элементов в список.');
    Writeln('2: Распечатка всего списка.');
    Writeln('3: Проверка списка на пустоту.');
    Writeln('4: Удаление элементов по условию.');
    Writeln('5: Очистка списка.');
    Writeln('6: Выход.');
    Write('Задайте команду: ');
    Readln(Cmd);
    case Cmd of
      1: WorkAdd(L);
      2:
      begin
        Writeln('Содержимое списка:');
        LWriteln(L);
      end;
      3:
        if L.PFirst = nil then
          Writeln('Список пуст.')
        else
          Writeln('Список непустой.');
      4:
      begin
        Writeln('Элементы с заданным значением будут удалены из списка.');
        Writeln('Чтобы отменить операцию оставьте пустую строку и нажмите Enter.');
        repeat
          Write('Значение: ');
          Readln(S);
          if S <> '' then
          begin
            Val(S, Data, Code);
            if Code = 0 then
            begin
              Cnt := DelByVal(L, Data);
              Writeln('Количество удалённых элементов: ', Cnt);
            end
            else
              Writeln('Неверный ввод. Повторите.');
          end;
        until S = '';
        Writeln('Диалог удаления завершён.')
      end;
      5, 6:
      begin
        ListFree(L);
        Writeln('Список удалён из памяти (очищен).');
        if Cmd = 6 then
          Writeln('Работа программы завершена.');
      end;
      else
        Writeln('Незарегистрированная команда. Повторите ввод.');
    end;
    Writeln('Для продолжения нажмите Enter.');
    Readln;
  until Cmd = 6;
end.
