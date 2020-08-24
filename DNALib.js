/*
	@fileoverview DNALibJS v2.4.0
  	@version 2.0
  	@license MIT
  	@copyright (c) 2019 Futurum, Inc
*/
//-> Преобразование ДНК - ДНК:
//    G == C  C == G  T == A  A == T
//-----------------------------
//-> Преобразование ДНК - иРНК: 
//    G == C  C == G  T == A  A == U
//-----------------------------
//-> Преобразование ДНК - тРНК: 
//    G == G  C == C  A == A  T == U
//-----------------------------
//-> Преобразование иРНК - тРНК: 
//    G == C  C == G  A == U  U == A
//-----------------------------
//-> Преобразование тРНК - иРНК: 
//    G == C  C == G  U == A A == U
// Опероны - кластеры (скопления) генов
// Сплайсинг - удаление интронов при формирования готовой иРНК
//---------------------------------
// Преобразование DNA -> DNA (ДНК -> ДНК)
// @param str - исходный код ДНК
// @return { string }
function dnaComplement (str) {
	let newStr = '';

	for (let i = 0; i <= str.length - 1; i++) {
		let s = str[i];

		if (s === 'A') {
			newStr += 'T';
		} else if (s === 'T') {
			newStr += 'A';
		} else if (s === 'C') {
			newStr += 'G';
		} else if (s === 'G') {
			newStr += 'C';
		} else {
			throw new Error('Invalid genetic code!');
		}
	}
	return newStr;
}
// Преобразование DNA -> uRNA (ДНК -> иРНК(мРНК)) - обычная РНК
// @param str - исходный код ДНК
// @return { string }
function dna_urnaComplement (str) {
    let newStr = '';

	for (let i = 0; i <= str.length - 1; i++) {
		let s = str[i];

		if (s === 'A') {
			newStr += 'U';
		} else if (s === 'T') {
			newStr += 'A';
		} else if (s === 'C') {
			newStr += 'G';
		} else if (s === 'G') {
			newStr += 'C';
		} else {
			throw new Error('Invalid genetic code!');
		}
	}
	return newStr;
}
// Преобразование DNA -> tRNA (ДНК -> тРНК)
// @param str - исходный код ДНК
// @return { string }
function dna_trnaComplement (str) {
	let newStr = '';

	for (let i = 0; i <= str.length - 1; i++) {
		let s = str[i];

		if (s === 'T') {
		    newStr += 'U';
		} else if (s === 'A' || s === 'C' || s === 'G') {
            newStr += s;
		} else {
			throw new Error('Invalid genetic code!');
		}
	}
	return newStr;
}

// Преобразование uRNA -> tRNA (иРНК -> тРНК)
// @param str - исходный код иРНК
// @return { string }
function urna_trnaComplement (str) {
	let newStr = '';

	for (let i = 0; i <= str.length - 1; i++) {
		let s = str[i];

		if (s === 'A') {
			newStr += 'U';
		} else if (s === 'U') {
			newStr += 'A';
		} else if (s === 'C') {
			newStr += 'G';
		} else if (s === 'G') {
			newStr += 'C';
		} else {
			throw new Error('Invalid genetic code!');
		}
	}
	return newStr;
}
/*
let dna_dna = dnaComplement('AATGTCTAGTTCCAGATC');
console.log(dna_dna); // TTACAGATCAAGGTCTAG
let dna_urna = dna_urnaComplement('TTACAGATCAAGGTCTAG');
console.log(dna_urna); // AAUGUCUAGUUCCAGAUC
let urna_trna = urna_trnaComplement('AAUGUCUAGUUCCAGAUC');
console.log(urna_trna); // UUACAGAUCAAGGUCUAG
let dna_trna = dna_trnaComplement('AATGTCTAGTTCCAGATC');
console.log(dna_trna); // AAUGUCUAGUUCCAGAUC
*/
//------------------- Комментарии в ДНК -------------
// код GT комментарии AG код
// исходный(незакомментированный) код - экзон
// начало комметарий ветвь комментарий GT - донор*
// комментарий - интрон
// конец комментария AG - акцептор**
// ветвь - указывает что комментарий скоро закончится
//------------ .c -> .o -> .exe
// ДНК нужен для получения РНК, а РНК необходим для получения белков, подобно тому, как из .c-файл превращается в объектный файл .o, который затем может быть скомпилирован в исполняемый файл (.exe).
// Как и при любой разработке проекта, происходило множество хаков, из-за которых информация иногда тече9т в обратном направлении. Иногда РНК патчит ДНК, а бывает, что ДНК модифицируется белками, созданными ранее.
// Промотор – последовательность, с которой связывается полимераза в процессе инициации транскрипции. Оператор // – это область, с которой могут связываться специальные белки – репрессоры, которые могут уменьшать // активность синтеза РНК с этого гена – иначе говоря, уменьшать его экспрессию.
//Терминатор – нетранскрибируемый участок ДНК в конце гена, на котором останавливается синтез РНК.
//----------------------------------------------------
// Является ли кодон инициаторным (start-кодон)
// @return { boolean }
function isInitiatorCodon (str) {
	const init_codons = ['ATG', 'GTG', 'TTG', 'AUG'];
	return init_codons.includes(str);
}
// Является ли кодон терминаторным (stop-кодон)
// @return { boolean }
function isTerminatorCodon (str) {
	const terminator_codons = ['UGA', 'UAG', 'UAA', 'TGA', 'TAG', 'TAA'];
	return terminator_codons.includes(str);
}
// Возвращает true, если это конец гена
// Н-р: isEndGen('UGA UAA UGA'); // true
// @return { boolean }
function isEndGen(str) {
	let codons = str.split(' ');
	if (isTerminatorCodon(codons[0]) && 
		isTerminatorCodon(codons[1]) && 
		isTerminatorCodon(codons[2])) {
		return true;
	} else {
		return false;
	}
}
// Трансляция генетического кода
// (Переводит последовательность кодонов иРНК в последовательность аминокислот)
// @return { string }
/*
Gly - глицин
Leu - лейцин
Tyr - тирозин
Ser - серин
Glu - глутаминовая кислота
Gln - глутамин
Asp - аспарагиновая кислота
Asn - аспарагин
Phe - фенилаланин
Ala - аланин
Lys - лизин
Arg - аргинин
His - гистидин
Cys - цистеин
Val - валин
Pro - пролин
Hyp - гидроксипролин
Trp - триптофан
Ile - изолейцин
Met - метионин
Thr - треонин
Hyl - гидроксилизин
*/
function convertCodonsToAminoasid (str) {
	// Массивы кодонов(ДНК-байтов)
	const gly_ = ['GGU', 'GGC', 'GGA', 'GGG'];
	const ala_ = ['GCU', 'GCC', 'GCA', 'GCG'];
	const val_ = ['GUU', 'GUC', 'GUA', 'GUG'];
	const ile_ = ['AUU', 'AUC', 'AUA'];
	const leu_ = ['UUA', 'UUG', 'CUU', 'CUC', 'CUA', 'CUG'];
	const pro_ = ['CCU', 'CCC', 'CCA', 'CCG'];
	const ser_ = ['UCU', 'UCC', 'UCA', 'UCG', 'AGU', 'AGC'];
	const thr_ = ['ACU', 'ACC', 'ACA', 'ACG'];
	const cys_ = ['UGU', 'UGC'];
	const met_ = ['AUG'];
	const asp_ = ['GAU', 'GAC'];
	const asn_ = ['AAU', 'AAC'];
	const glu_ = ['GAA', 'GAG'];
	const gln_ = ['CAA', 'CAG'];
	const lys_ = ['AAA', 'AAG'];
	const arg_ = ['CGU', 'CGC', 'CGA', 'CGG', 'AGA', 'AGG'];
	const his_ = ['CAU', 'CAC'];
	const phe_ = ['UUU', 'UUC'];
	const tyr_ = ['UAU', 'UAC'];
	const trp_ = ['UGG'];

	let codons = str.split(' ');
	let newStr = '';

    function isfindadder (array_codons, current_codon, addValue) {
    	if (array_codons.includes(current_codon)){
    		newStr += addValue;
    	}
    }

	codons.forEach( function(elem, index) {
        isfindadder(gly_, elem, ' Gly ');
        isfindadder(ala_, elem, ' Ala ');
        isfindadder(val_, elem, ' Val ');
        isfindadder(ile_, elem, ' Ile ');
        isfindadder(leu_, elem, ' Leu ');
        isfindadder(pro_, elem, ' Pro ');
        isfindadder(ser_, elem, ' Ser ');
        isfindadder(thr_, elem, ' Thr ');
        isfindadder(cys_, elem, ' Cys ');
        isfindadder(met_, elem, ' Met ');
        isfindadder(asp_, elem, ' Asp ');
        isfindadder(asn_, elem, ' Asn ');
        isfindadder(glu_, elem, ' Glu ');
        isfindadder(gln_, elem, ' Gln ');
        isfindadder(lys_, elem, ' Lys ');
        isfindadder(arg_, elem, ' Arg ');
        isfindadder(his_, elem, ' His ');
        isfindadder(phe_, elem, ' Phe ');
        isfindadder(tyr_, elem, ' Tyr ');
        isfindadder(trp_, elem, ' Trp ');
	});
	return newStr;
}
// console.log(convertCodonsToAminoasid ('AUG ACC GCA UAU AUC CAU'));//Met  Thr  Ala  Tyr  Ile  His
// Дупликация генетического кода
// H-p: duplication('AUGACAC', 2,4); // AUGAGACAC
// @param str - строка генетического кода
// @param start - позиция с которой будет дуплироваться подстрока кодв до позиции end
// @return { string }
function duplication (str, start, end) {
	// часть строки до дублируемой подстроки
	let beforeDup = str.slice(0, end);
	// подстрока для дупликации
	let subsr = str.slice(start, end);
	// часть строки после дублируемой подстроки
	let afterDup = str.slice(end, str.length);
	let resultString = beforeDup.concat(subsr, afterDup);
	return resultString;
}
// Инверсия гентического кода
// H-p: invertion('AUGACC', 1, 3); // AGUACC
// @param str - строка генетического кода
// @param start - позиция с которой будет инвертироваться подстрока кодв до позиции end
// @return { string }
function invertion (str, start, end) {
	let beforeInv = str.slice(0, start);
	let rev = str.slice(start, end).split('').reverse().join(''); // реверсия указанной подстроки
	let afterInv = str.slice(end, str.length);
	let resultString = beforeInv.concat(rev, afterInv);
	return resultString;
}
// Является ли участок кода интроном
// @param str - строка для проверки
// @return { boolean } 
function isIntron(str) {
	let isGU = str[0] == 'G' && str[1] == 'U';
	let isAG = str[str.length - 2] == 'A' && str[str.length - 1] == 'G';
	if ( isGU && isAG) {
		return true;
	}
	return false;
}
// Убирает все пробелы из строки
// @return { string }
function unformat(str) {
	return str.replace(/\s+/gi, '');
}
// Делает строку более удобной для чтения
// @return { string }
function format(str) {
	let arr = str.match(/\w{3}/gi);
	let formatStr = '';

	arr.forEach( function(elem, index) {
		formatStr += elem + ' ';
	});

	return formatStr;
}
// Функция вызова ошибки
function restr_error(restrictaza) {
	throw new Error(`Error: Рестриктаза ${restrictaza} не применима к данному генетическому коду.`);
}
// Возвращает позицию сайта узнавания для рестрикции
// @return { number(int) }
function getPositionRecognitionSite (genetic_code, recognition_site) {
	if (genetic_code.length > 0 && typeof recognition_site === 'string') {
		return genetic_code.indexOf(recognition_site);
	} else {
		throw new Error('Error: getPositionRecognitionSite() - невозможно определить позицию сайта узнавания');
	}
}
// Возвращает части генетического кода после рестрикции
// @return { string[2] }
function getLeftAndRightParts (genetic_code, left_start, left_end, right_start, right_end) {
	const left_part = genetic_code.slice(left_start, left_end);
	const right_part = genetic_code.slice(right_start, right_end);
	return [left_part, right_part];
}
// В функциях ниже знак ^ означает место разреза ДНК

// Рестриктаза Eco RI, распознает сайт узнавания: G^AATTC
// @param str - строка генетического кода
// @return { string[2] }
function ecoRI (str) {
	if (/GAATTC/.test(str)) {
		let position = getPositionRecognitionSite(str, 'GAATTC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('eco RI');
	}
}
// Рестриктаза Hind III, распознает сайт узнавания: A^AGCTT или TTCGA^A
// @param str - строка генетического кода
// @return { string[2] }
function hindIII (str) {
	let position;

	if (/AAGCTT/.test(str)) {
		position = getPositionRecognitionSite(str, 'AAGCTT');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/TTCGAA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TTCGAA');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else {
		restr_error('Hind III');
	}
}
// Рестриктаза Bam I, распознает сайт узнавания: G^GATCC
// @param str - строка генетического кода
// @return { string[2] }
function bamI (str) {
	if (/GGATCC/.test(str)) {
		let position = getPositionRecognitionSite(str, 'GGATCC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('bam I');
	}
}
// рестриктаза Hae III, распознает сайт узнавания: GG^CC
// @param str - строка генетического кода
// @return { string[2] }
function haeIII (str) {
	if (/GGCC/.test(str)) {
		let position = getPositionRecognitionSite(str, 'GGCC');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else {
		restr_error('hae III');
	}
}
// рестриктаза Hpa II, распознает сайт узнавания: C^CGG
// @param str - строка генетического кода
// @return { string[2] }
function hpaII (str) {
    if (/CCGG/.test(str)) {
		let position = getPositionRecognitionSite(str, 'CCGG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('hpa II');
	}
}
// рестриктаза Sma I, распознает сайт узнавания: CCC^GGG
// @param str - строка генетического кода
// @return { string[2] }
function smaI (str) {
	if (/CCCGGG/.test(str)) {
		let position = getPositionRecognitionSite(str, 'CCCGGG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('sma I');
	}
}
// Рестриктаза Acc65 I, распознает сайт узнавания: G^GTACC
// @param str - строка генетического кода
// @return { string[2] }
function acc65I (str) {
	if (/GGTACC/.test(str)) {
		let position = getPositionRecognitionSite(str, 'GGTACC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Acc65 I');
	}
}
// Рестриктаза Ahl I, распознает сайт узнавания: A^CTAGT или TGATC^A
// @param str - строка генетического кода
// @return { string[2] } 
function ahlI (str) {
	let position;

	if (/ACTAGT/.test(str)) {
		position = getPositionRecognitionSite(str, 'ACTAGT');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/TGATCA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TGATCA');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else {
		restr_error('Ahl I');
	}
}
// Рестриктаза Alu I, распознает сайт узнавания: AG^CT
// @param str - строка генетического кода
// @return { string[2] }
function aluI (str) {
	if (/AGCT/.test(str)) {
		let position = getPositionRecognitionSite(str, 'AGCT');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else {
		restr_error('Alu I');
	}
}
// Рестриктаза Apa I, распознает сайт узнавания: GGGCC^C или C^CCGGG
// @param str - строка генетического кода
// @return { string[2] }
function apaI (str) {
	let position;

	if (/GGGCCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGGCCC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/CCCGGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCCGGG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Apa I');
	}
}
// Рестриктаза AsiG I, распознает сайт узнавания: A^CCGGT
// @param str - строка генетического кода
// @return { string[2] }
function asiGI (str) {
	if (/ACCGGT/.test(str)) {
		let position = getPositionRecognitionSite(str, 'ACCGGT');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('AsiG I');
	}
}
// Рестриктаза Aat II, распознает сайт узнавания: GACGT^C или C^TGCAG
// @param str - строка генетического кода
// @return { string[2] }
function aatII (str) {
	let position;

	if (/GACGTC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GACGTC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/CTGCAG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CTGCAG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Aat II');
	}
}
// Рестриктаза Abs I, распознает сайт узнавания: CC^TCGAGG или GGAGCT^CC
// @param str - строка генетического кода
// @return { string[2] }
function absI (str) {
	let position;

	if (/CCTCGAGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCTCGAGG');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else if (/GGAGCTCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGAGCTCC');
		return getLeftAndRightParts(str, 0, position + 6, position + 6, str.length);
	} else {
		restr_error('Abs I');
	}
}
// Рестриктаза Acc16 I, распознает сайт узнавания: TGC^GCA или ACG^CGT
// @param str - строка генетического кода
// @return { string[2] }
function acc16I (str) {
	let position;

	if (/TGCGCA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TGCGCA');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/ACGCGT/.test(str)) {
		position = getPositionRecognitionSite(str, 'ACGCGT');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Acc16 I');
	}
}
// Рестриктаза Acc65 I, распознает сайт узнавания: G^GTACC или CCATG^G
// @param str - строка генетического кода
// @return { string[2] }
function acc65I (str) {
	let position;

	if (/GGTACC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGTACC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/CCATGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCATGG');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else {
		restr_error('Acc65 I');
	}
}
// Рестриктаза AccBS I, распознает сайт узнавания: GAG^CGG или CTC^GCC
// @param str - строка генетического кода
// @return { string[2] }
function accBSI (str) {
	let position;

	if (/GAGCGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'GAGCGG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/CTCGCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'CTCGCC');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('AccBS I');
	}
}
// Рестриктаза Acl I, распознает сайт узнавания: AA^CGTT или TTGC^AA
// @param str - строка генетического кода
// @return { string[2] }
function aclI (str) {
	let position;

	if (/AACGTT/.test(str)) {
		position = getPositionRecognitionSite(str, 'AACGTT');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else if (/TTGCAA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TTGCAA');
		return getLeftAndRightParts(str, 0, position + 4, position + 4, str.length);
	} else {
		restr_error('Acl I');
	}
}
// Рестриктаза Afe I, распознает сайт узнавания: AGC^GCT или TCG^CGA
// @param str - строка генетического кода
// @return { string[2] }
function afeI (str) {
	let position;

	if (/AGCGCT/.test(str)) {
		position = getPositionRecognitionSite(str, 'AGCGCT');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/TCGCGA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TCGCGA');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Afe I');
	}
}
// Рестриктаза AsiS I, распознает сайт узнавания: GCGAT^CGC или CGC^TAGCG
// @param str - строка генетического кода
// @return { string[2] }
function asisI (str) {
	let position;

	if (/GCGATCGC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GCGATCGC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/CGCTAGCG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CGCTAGCG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('AsiS I');
	}
}
// Рестриктаза AspA2 I, распознает сайт узнавания: C^CTAGG или GGATC^C
// @param str - строка генетического кода
// @return { string[2] }
function aspA2I (str) {
	let position;

	if (/CCTAGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCTAGG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/GGATCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGATCC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else {
		restr_error('AspA2 I');
	}
}
// Рестриктаза Bmp I, распознает сайт узнавания: GCTAG^C или C^GATCG 
// @param str - строка генетического кода
// @return { string[2] }
function bmtI (str) {
	let position;

	if (/GCTAGC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GCTAGC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/CGATCG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CGATCG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Bmt I');
	}
}
// Рестриктаза Btr I, распознает сайт узнавания: CAC^GTC или GTG^CAG
// @param str - строка генетического кода
// @return { string[2] }
function btrI (str) {
	let position;

	if (/CACGTC/.test(str)) {
		position = getPositionRecognitionSite(str, 'CACGTC');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/GTGCAG/.test(str)) {
		position = getPositionRecognitionSite(str, 'GTGCAG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Btr I');
	}
}
// Рестриктаза BsuR I, распознает сайт узнавания: GG^CC или CC^GG
// @param str - строка генетического кода
// @return { string[2] }
function bsuRI (str) {
	let position;

	if (/GGCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGCC');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else if (/CCGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCGG');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else {
		restr_error('BsuR I');
	}
}
// Рестриктаза Dra I, распознает сайт узнавания: TTT^AAA или AAA^TTT
// @param str - строка генетического кода
// @return { string[2] }
function draI (str) {
	let position;

	if (/TTTAAA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TTTAAA');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/AAATTT/.test(str)) {
		position = getPositionRecognitionSite(str, 'AAATTT');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Dra I');
	}
}
// Рестриктаза Ege I, распознает сайт узнавания: GGC^GCC или CCG^CGG
// @param str - строка генетического кода
// @return { string[2] }
function egeI (str) {
	let position;

	if (/GGCGCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGCGCC');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/CCGCGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCGCGG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Ege I');
	}
}
// Рестриктаза Fae I, распознает сайт узнавания: CATG^ или ^GTAC
// @param str - строка генетического кода
// @return { string[2] }
function faeI (str) {
	let position;

	if (/CATG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CATG');
		return getLeftAndRightParts(str, 0, position + 4, position + 4, str.length);
	} else if (/GTAC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GTAC');
		return getLeftAndRightParts(str, 0, position, position , str.length);
	} else {
		restr_error('Fae I');
	}
}
// Рестриктаза Gsa I, распознает сайт узнавания: CCCAG^C или G^GGTCG
// @param str - строка генетического кода
// @return { string[2] }
function gsaI (str) {
	let position;

	if (/CCCAGC/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCCAGC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/ GGGTCG/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGGTCG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Gsa I');
	}
}
// Рестриктаза Hpa I, распознает сайт узнавания: GTT^AAC или CAA^TTG
// @param str - строка генетического кода
// @return { string[2] }
function hpaI (str) {
	let position;

	if (/GTTAAC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GTTAAC');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/CAATTG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CAATTG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('Hpa I');
	}
}
// Рестриктаза Mly113 I, распознает сайт узнавания: GG^CGCC или CCGC^GG
// @param str - строка генетического кода
// @return { string[2] }
function mly113I (str) {
	let position;

	if (/GGCGCC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGCGCC');
		return getLeftAndRightParts(str, 0, position + 2, position + 2, str.length);
	} else if (/CCGCGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCGCGG');
		return getLeftAndRightParts(str, 0, position + 4, position + 4, str.length);
	} else {
		restr_error('Mly113 I');
	}
}
// Рестриктаза XbaI, распознает сайт узнавания: T^CTAGA
// @param str - строка генетического кода
// @return { string[2] }
function xbaI (str) {
	if (/TCTAGA/.test(str)) {
		let position = getPositionRecognitionSite(str, 'TCTAGA');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('XbaI');
	}
}
// Рестриктаза KpnI, распознает сайт узнавания: GGTAC^C или C^CATGG
// @param str - строка генетического кода
// @return { string[2] }
function kpnI (str) {
	let position;

	if (/GGTACC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GGTACC');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else if (/CCATGG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CCATGG');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('KpnI');
	}
}
// Рестриктаза HpySE526 I, распознает сайт узнавания: A^CGT или TGC^A
// @param str - строка генетического кода
// @return { string[2] }
function hpySE526I (str) {
	let position;

	if (/ACGT/.test(str)) {
		position = getPositionRecognitionSite(str, 'ACGT');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/TGCA/.test(str)) {
		position = getPositionRecognitionSite(str, 'TGCA');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('HpySE526 I');
	}
}
// Рестриктаза Xma I, распознает сайт узнавания: G^GGCCC
// @param str - строка генетического кода
// @return { string[2] }
function xmaI (str) {
	if (/GGGCCC/.test(str)) {
		let position = getPositionRecognitionSite(str, 'GGGCCC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else {
		restr_error('Xma I');
	}
}
// Рестриктаза EcoR V, распознает сайт узнавания: GAT^ATC CTA^TAG
// @param str - строка генетического кода
// @return { string[2] }
function ecoRV (str) {
	let position;

	if (/GATATC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GATATC');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else if (/CTATAG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CTATAG');
		return getLeftAndRightParts(str, 0, position + 3, position + 3, str.length);
	} else {
		restr_error('EcoR V');
	}
}
// Рестриктаза Sal I, распознает сайт узнавания: G^TCGAC CAGCT^G
// @param str - строка генетического кода
// @return { string[2] }
function salI (str) {
	let position;

	if (/GTCGAC/.test(str)) {
		position = getPositionRecognitionSite(str, 'GTCGAC');
		return getLeftAndRightParts(str, 0, position + 1, position + 1, str.length);
	} else if (/CAGCTG/.test(str)) {
		position = getPositionRecognitionSite(str, 'CAGCTG');
		return getLeftAndRightParts(str, 0, position + 5, position + 5, str.length);
	} else {
		restr_error('Sal I');
	}
}
//-------------------------------------------------------------
// Вызывает нужную функцию рестрикции для участка ДНК
// @param restrictaza - рестриктаза для получения фрагментов ДНК
// @param str_code - строка генетического кода
// @return { string[2] }
function restriction (restrictaza, str_code) {
	return restrictaza(str_code);
}
// лигирование – сшивание ферментом лигазой плазмидного (векторного) и чужеродного фрагментов ДНК; 
// при этом концы плазмидной (векторной) и чужеродной ДНК комплементарны друг другу
// и поэтому называются «липкие концы»
// @param plasmid - участок ДНК, к которому присоединяем foreign
// @param foreign - чужой фрагмент ДНК
// @return { string }
function ligare (plasmid, foreign) {
	return plasmid.concat(foreign);
}

//-----------------------------------------------------------------------
/*
 	Условные Обозначения:
R = A или G
W = A или T
S = G или C
K = G или T
M = A или C
Y = T или C
B = C или G или T
D = A или G или T
H = A или C или T
V = A или C или G
N = A или C или G или T 
*/

// Рестриктаза Hinf I Сайт узнавания: G^ANTC или CTNA^G
// Рестриктаза BamH I Сайт узнавания: G^GATCC CCTAG^G
// Рестриктаза BssT1 I Сайт узнавания: C^CWWGG GGWWC^C
// Рестриктаза Erh I Сайт узнавания: C^CWWGG GGWWC^C
// Sph I (CGTAC^G)
// Bbu I (CGTAC^G)
/*
BamH I G^GATCC или CCTAG^G 
BglII A^GATCT или TCTAG^A
Bsa29I AT^CGAT 
Bsp19I C^CATGG 
CciNI GC^GGCCGC
EcoRV GAT^ATC 
FauNDI CA^TATG 
HindIII A^AGCTT
HinfI G^ANTC 
HpaI GT^TAAC
MluI A^CGCGT
PceI AGG^CCT
Psp124BI GAGCT^C
PstI CTGCA^G
SalI G^TCGAC
Sfr274I C^TCGAG
SphI GCATG^C

Acc B1 I G^GYRCC или CCRYG^G
Aco I Y^GGCCR
Ajn I ^CCWGG или GGWCC^ 
 
AspLE I GCG^C или C^GCG
AsuNH I G^CTAGC или CGATC^G
Bpu14 I TT^CGAA или AAGC^TT
Bsa29 I AT^CGAT или TAGC^TA
BseP I G^CGCGC или CGCGC^G
BseX3 I C^GGCCG или GCCGG^C
Bsp13 I T^CCGGA или AGGCC^T 
BspAC I C^CGC или GGC^G 
BspFN I CG^CG или GC^GC 
BssNA I GTA^TAC или CAT^ATG 
Bst2B I C^TCGTG или GAGCA^C
BstAU I T^GTACA или ACATG^T 
BstHH I GCG^C или C^GCG 
BstKT I GAT^C или C^TAG
BstMB I ^GATC или CTAG^
BstSN I TAC^GTA или ATG^CAT
Cci I T^CATGA или AGTAC^T 
CciN I GC^GGCCGC или CGCCGG^CG 
EcolCR I GAG^CTC или CTC^GAG 
FauND I CA^TATG или GTAT^AC 

HspA I G^CGC или CGC^G 
Ksp22 I T^GATCA или ACTAG^T 
Kzo9 I ^GATC или CTAG^ 
Mfe I C^AATTG или GTTAA^C 
Mox20 I TGG^CCA или ACC^GGT 
MroN I G^CCGGC или CGGCC^G
Msp I C^CGG или GGC^C
Nru I TCG^CGA или AGC^GCT 
PalA I GG^CGCGCC или CCGCGC^GG
Pce I AGG^CCT или TCC^GGA
Pci I A^CATGT или TGTAC^A 
Ple19 I CGAT^CG или GC^TAGC 
Psi I TTA^TAA или AAT^ATT 
Psp124B I GAGCT^C или C^TCGAG 
PspC I CAC^GTG или GTG^CAC 
PspL I C^GTACG или GCATG^C
PspOM I G^GGCCC или CCCGG^G 
Pst I CTGCA^G или G^ACGTC 
Pvu II CAG^CTG или GTC^GAC
Rga I GCGAT^CGC или CGC^TAGCG 
Rig I GGCCGG^CC или CC^GGCCGG 
Rsa I GT^AC или CA^TG 
RsaN I G^TAC или CAT^G
Sbf I CCTGCA^GG или GG^ACGTCC 
Set I ASST^ или ^TSSA
Sfr274 I C^TCGAG или GAGCT^C 
Sfr303 I CCGC^GG или GG^CGCC 
Smi I ATTT^AAAT или TAAA^TTTA 
Sse9 I ^AATT или TTAA^ 
Ssp I AAT^ATT или TTA^TAA
SspM I C^TAG или GAT^C 
Taq I T^CGA или AGC^T 
Tru9 I T^TAA или AAT^T 
Vne I G^TGCAC или CACGT^G
Vsp I AT^TAAT или TAAT^TA 
Xba I T^CTAGA или AGATC^T
Zra I GAC^GTC или CTG^CAG
Zrm I AGT^ACT или TCA^TGA
Zsp2 I ATGCA^T или T^ACGTA
*/
//----------------------------------------------
// Вызов функции рестрикции ДНК с использованием разных рестриктаз
//console.log(restriction(hindIII, 'AAGCTTGGGAAGCTT'));

/*
cla I - AT^CGAT TAGC^TA
dpn II - ^GATC CTAG^
eco47 III - AGC^GCT TCG^CGA
nco I - C^CATGG GGTAC^C
nde I - CA^TATG GTAT^AC
nhe I - G^CTAGC CGATC^G
not I - GC^GGCCGC CGCCGG^CG
sac I - GAGCT^C C^TCGAG
Sau3A I - ^GATC CTAG^
Sfi I - GGCCNNNN^NGGCC CCGGN^NNNNCCGG
xho I - C^TCGAG GAGCT^C
Aar I - CACCTGCNNNN^ GTGGACGNNNNNNNN^
Aas I - GACNNNN^NNGTC CTGNN^NNNNCAG
Aat II - GACGT^C C^TGCAG
Ade I - CAC NNN^GTG GTG^NNN CAC
TurboNae I - GCC^GGC CGG^CCG
TurboNar I - GG^CGCC CCGC^GG
EcoO109 I - RG^GNCCY YCCNG^GR
Tth111 I - GACN^NNGTC CTGNN^NCAG
Eam1104 I - CTCTTCN^ GAGAAGNNNN^
Eam1105 I - GACNNN^NNGTC CTGNN^NNNCAG
Taa I - ACN^GT TG^NCA
*/
