class PHPAdapter {
	// Здесь функции PHP, переписанные на JS
}

// Функция может принимать произвольное число массивов на входе
// Возвращает элементы массивов, которые уникальны и не повторяются
function array_diff(arr1) {
	let retArr = {}
    let argl = arguments.length
    let k1 = ''
    let i = 1
    let k = ''
    let arr = {}

  arr1keys: for (k1 in arr1) { 
    for (i = 1; i < argl; i++) {
      arr = arguments[i]
      for (k in arr) {
        if (arr[k] === arr1[k1]) {
          continue arr1keys 
        }
      }
      retArr[k1] = arr1[k1]
    }
  }

  return retArr
}

// Функция может принимать произвольное число объектов на входе
// Возвращает новый объект, который содержит уникальные значения
function array_diff_assoc(arr1) {
	let retArr = {};
	let argl = arguments.length;
	let k1 = '';
	let i = 1;
	let k = '';
	let arr = {};

	arr1keys: for (k1 in arr1) {
		for (i = 1; i < argl; i++) {
			arr = arguments[i];
			for (k in arr) {
				if (arr[k] === arr1[k1] && k === k1) {
					continue arr1keys;
				}
			}
			retArr[k1] = arr1[k1];
		}
	}
	return retArr;
}

// Функция может принимать произвольное число объектов на входе
// Возвращает новый объект, который содержит уникальные ключи
function array_diff_key(arr1) {
	let retArr = {};
	let argl = arguments.length;
	let k1 = '';
	let i = 1;
	let k = '';
	let arr = {};

	arr1keys: for (k1 in arr1) {
		for (i = 1; i < argl; i++) {
			arr = arguments[i];
			for (k in arr) {
				if (k === k1) {
					continue arr1keys;
				}
			}
			retArr[k1] = arr1[k1];
		}
	}
	return retArr;
}
// ?
function array_diff_uassoc (arr1) {
	let retArr = {};
	let arglml = arguments.length - 1;
	let cb = arguments[arglml];
	let arr = {};
	let i = 1;
	let k1 = '';
	let k = '';

	let $global = (typeof window !== 'undefined' ? window : global);

	cb = (typeof cb === 'string')
		? $global[cb] 
		: (Object.prototype.toString.call(cb) === '[object Array]')
			? $global[cb[0]][cb[1]]
			: cb;

	arr1keys: for (k1 in arr1) {
		for (i = 1; i < arglml; i++) {
			arr = arguments[i];
			for (k in arr) {
				if (arr[k] === arr1[k1] && cb(k,k1) === 0) {
					continue arr1keys;
				}
			}
			retArr[k1] = arr1[k1];
		}
	}
	return retArr;
}
// Функция может принимать на вход объект array
// Возвращает объект, регистр ключей которого изменен на указанный case_
// case_ может принимать значения:
//   CASE_LOWER
//   CASE_UPPER
function array_change_key_case (array, case_) {
	let caseFunc, key;
	let tmpArr = {};

	if (Object.prototype.toString.call(array) === '[object Array]') {
		return array;
	}

	if (array && typeof array === 'object') {
		caseFunc = (!case_ || case_ === 'CASE_LOWER')? 'toLowerCase': 'toUpperCase';
		for (key in array) {
			tmpArr[key[caseFunc]()] = array[key];
		}
		return tmpArr;
	}
	return false;
}
// Функция возвращает:
// Если preserveKeys = true && input = массив, то возвращает массив с указанной длиной, а остальные элементы выносит во внутренний массив
// Если preserveKeys = false && input = массив, то возвращает объект с указанной длиной, а остальные элементы выносит во внутренний объект
// Если preserveKeys = true && input = объект, то возвращает объект с указанной длиной, а остальные элементы выносит во внутренний объект 
// Если preserveKeys = false && input = объект, то возвращает массив с указанной длиной, а остальные элементы выносит во внутренний массив 
function array_chunk (input, size, preserveKeys) {
	let x;
	let p = '';
	let i = 0;
	let c = -1;
	let l = input.length || 0;
	let n = [];

	if (size < 1) {
		return null;
	}

	if (Object.prototype.toString.call(input) === '[object Array]') {
		if (preserveKeys) {
			while (i < l) {
				(x = i % size)? n[c][i] = input[i]: n[++c]= {};
				n[c][i] = input[i];
				i++;
			}
		} else {
			while (i < l) {
				(x = i % size)? n[c][x] = input[i]: n[++c] = [input[i]];
				i++;
			}
		}
	} else {
		if (preserveKeys) {
			for (p in input) {
				if (input.hasOwnProperty(p)) {
					(x = i % size)? n[c][i] = input[p]: n[++c] = {};
					n[c][p] = input[p];
					i++;
				}
			}
		} else {
			for (p in input) {
				if (input.hasOwnProperty(p)) {
					(x = i % size)? n[c][x] = input[p]: n[++c] = [input[p]];
					i++;
				}
			}
		}
	}
	return n;
}
// 
function array_column (input, ColumnKey, IndexKey = null) {
	if (input !== null && (typeof input === 'object' || Array.isArray(input))) {
		let newarray = [];

		if (typeof input === 'object') {
			let temparray = [];
			for (let key of Object.keys(input)) {
				temparray.push(input[key]);
			}
			input = temparray;
		}

		if (Array.isArray(input)) {
			for (let key of input.keys()) {
				if (IndexKey && input[key][IndexKey]) {
					if (ColumnKey) {
						newarray[input[key][IndexKey]] = input[key][ColumnKey];
					} else {
						newarray[input[key][IndexKey]] = input[key];
					}
				} else {
					if (ColumnKey) {
						newarray.push(input[key][ColumnKey]);
					} else {
						newarray.push(input[key]);
					}
				}
			}
		}
		return Object.assign({}, newarray);
	}
}
// Функция, возвращает объект, в котором
// ключи - уникальные ключи из массива-параметра,
// значения - кол-во повторяющихся значений массива-параметра
function array_count_values (array) {
	let tmpArr = {};
	let key = '';
	let t = '';

	let _getType = function (obj) {
		let t = typeof obj;
		t = t.toLowerCase();
		if (t === 'object') {
			t = 'array';
		}
		return t;
	}

	let _countValue = function (tmpArr, value) {
		if (typeof value === 'number') {
			if (Math.floor(value) !== value){
				return;
			}
		} else if (typeof value !== 'string') {
			return;
		}

		if (value in tmpArr && tmpArr.hasOwnProperty(value)) {
			++tmpArr[value];
		} else {
			tmpArr[value] = 1;
		}
	}

	t = _getType(array);
	if (t === 'array') {
		for(key in array) {
			if (array.hasOwnProperty(key)) {
				_countValue.call(this, tmpArr, array[key]);
			}
		}
	}
	return tmpArr;
}
//
function array_diff_ukey (arr1) {
	let retArr = {};
	let arglml = arguments.length - 1;
	let cb = arguments[arglml];
	let k1 = '';
	let i = 1;
	let arr = {};
	let k = '';

	let $global = (typeof window !== 'undefined' ? window : global);

	cb = (typeof cb === 'string')
		? $global[cb]
		: (Object.prototype.toString.call(cb) === 'object Array')
			? $global[cb[0]][cb[1]]
			: cb;

	arr1keys: for (k1 in arr1) {
		for (i = 1; i < arglml; i++) {
			arr = arguments[i];
			for (k in arr) {
				if (cb(k, k1) === 0) {
					continue arr1keys;
				}
			}
			retArr[k1] = arr1[k1];
		}
	}
	return retArr;
}
// Создает объект с num ключами начинающимися с startIndex.
// Все значения заполняет mixedValue
function array_fill (startIndex, num, mixedValue) {
	let key;
	let tmpArr = {};

	if (!isNaN(startIndex) && !isNaN(num)) {
		for (key = 0; key < num; key++) {
			tmpArr[(key + startIndex)] = mixedValue;
		}
	}
	return tmpArr;
}

// Возвращает объект с измененными значениями
function array_fill_keys (keys, value) {
	let retObj = {};
	let key = '';

	for (key in keys) {
		retObj[keys[key]] = value;
	}
	return retObj;
}
// Производит обмен местами ключей и значений массива
function array_flip (transArr) {
	let key;
	let tmpArr = {};

	for (key in transArr) {
		if (!transArr.hasOwnProperty(key)) {
			continue;
		}
		tmpArr[transArr[key]] = key;
	}
	return tmpArr;
}
// Возвращает true если объект search содержит ключ key, иначе false
function array_key_exists (key, search) {
	if (!search || (search.constructor !== Array && search.constructor !== Object)) {
		return false;
	}
	return key in search;
}
// Соединяет два или несколько массивов в один новый массив
function array_merge () {
	let args = Array.prototype.slice.call(arguments);
	let argl = args.length;
	let arg;
	let retObj = {};
	let k = '';
	let argil = 0;
	let j = 0;
	let i = 0;
	let ct = 0;
	let toStr = Object.prototype.toString;
	let retArr = true;

	for (i = 0; i < argl; i++) {
		if (toStr.call(args[i]) !== '[object Array]') {
			retArr = false;
			break;
		}
	}

	if (retArr) {
		retArr = [];
		for (i = 0; i < argl; i++) {
			retArr = retArr.concat(args[i]);
		}
		return retArr;
	}

	for (i = 0, ct = 0; i < argl; i++) {
		arg = args[i];
		if (toStr.call(arg) === '[object Array]') {
			for (j = 0, argil = arg.length; j < argil; j++) {
				retObj[cr++] = arg[j];
			}
		} else {
			for (k in arg) {
				if (arg.hasOwnProperty(k)) {
          			if (parseInt(k, 10) + '' === k) {
            			retObj[ct++] = arg[k]
          			} else {
            			retObj[k] = arg[k]
		          	}
		        }
			}
		}
	}
	return retObj;
}
// Увеличить размер массива до заданной величины
function array_pad (input, padSize, padValue) {
	let pad = [];
	let newArray = [];
	let newLength;
	let diff = 0;
	let i = 0;

	if (Object.prototype.toString.call(input) === '[object Array]' && !isNaN(padSize)) {
    newLength = ((padSize < 0) ? (padSize * -1) : padSize)
    diff = newLength - input.length

    if (diff > 0) {
      for (i = 0; i < diff; i++) {
        newArray[i] = padValue
      }
      pad = ((padSize < 0) ? newArray.concat(input) : input.concat(newArray))
    } else {
      pad = input
    }
  }

  return pad
}
// Возвращает произведение значений массива как целое число или число с плавающей точкой
function array_product (array) {
	let product = 1;
	let al = 0;
	let i = 0;

	if (Object.prototype.toString.call(array) !== '[object Array]') {
		return null;
	}
	al = array.length;
	while (i < al) {
		product *= (!isNaN(array[i])? array[i]: 0);
		i++;
	}
	return product;
}
// Возвращает сумму значений массива
function array_sum (array) {
	let sum = 0;

	if (typeof array !== 'object') {
		return null;
	}

	for (let key in array) {
		if (!isNaN(parseFloat(array[key]))) {
			sum += parseFloat(array[key]);
		}
	}
	return sum;
}
// Возвращает индекс найденного символа в строке
// Добавлена из Go
function Index(s, sep) {
	return (s + '').indexOf(sep);
}
// Разлагает число num на мантиссу в пределах от 0.5 до величины, меньшей, чем 1, и на целую экспоненту, так что выполняется равенство num = mantissa * 2ехр
// Добавлена из C
function frexp (arg) {
	arg = Number(arg)

  	const result = [arg, 0]

  	if (arg !== 0 && Number.isFinite(arg)) {
    	const absArg = Math.abs(arg)
    	const log2 = Math.log2 || function log2 (n) { return Math.log(n) * Math.LOG2E }
    	let exp = Math.max(-1023, Math.floor(log2(absArg)) + 1)
    	let x = absArg * Math.pow(2, -exp)

        while (x < 0.5) {
      		x *= 2
      		exp--
    	}
    	while (x >= 1) {
      		x *= 0.5
      		exp++
    	}

    	if (arg < 0) {
      		x = -x
    	}
    	result[0] = x
    	result[1] = exp
  	}
  return result
}
// Возвращает логическое значение для выражения
function boolval (mixedValue) {
	if (mixedValue === false){
		return false;
	}

	if (mixedValue === 0 || mixedValue === 0.0) {
		return false;
	}

	if (mixedValue === '' || mixedValue === '0') {
		return false;
	}

	if (Array.isArray(mixedValue) && mixedValue.length === 0) {
		return false;
	}

	if (mixedValue === null || mixedValue === undefined) {
		return false;
	}
	return true;
}
// Возвращает значение с плавающей запятой
function doubleval (mixedValue) {
	return (parseFloat(mixedValue) || 0);
}
// Является ли переменная true или false
function is_bool (mixedValue) {
	return (mixedValue === true || mixedValue === false);
}

function is_object (mixedValue) {
	if (Object.prototype.toString.call(mixedValue) === '[object Array]') {
		return false;
	}

	return mixedValue !== null && typeof mixedValue === 'object';
}

function is_scalar (mixedValue) {
	return (/boolean|number|string/).test(typeof mixedValue);
}

function is_null (mixedValue) {
	return (mixedValue === null);
}

function is_buffer (mixedValue) {
	return typeof mixedValue === 'string';
}

function is_unicode (mixedValue) {
	if (typeof mixedValue !== 'string') {
		return false;
	}

	let arr = [];
    let highSurrogate = '[\uD800-\uDBFF]';
    let lowSurrogate = '[\uDC00-\uDFFF]';
    let highSurrogateBeforeAny = new RegExp(highSurrogate + '([\\s\\S])', 'g');
    let lowSurrogateAfterAny = new RegExp('([\\s\\S])' + lowSurrogate, 'g');
    let singleLowSurrogate = new RegExp('^' + lowSurrogate + '$');
    let singleHighSurrogate = new RegExp('^' + highSurrogate + '$');

  	while ((arr = highSurrogateBeforeAny.exec(mixedValue)) !== null) {
    	if (!arr[1] || !arr[1].match(singleLowSurrogate)) {
      		return false;
    	}
  	}
  	while ((arr = lowSurrogateAfterAny.exec(mixedValue)) !== null) {
    	if (!arr[1] || !arr[1].match(singleHighSurrogate)) {
      		return false;
    	}
  	}

  	return true;
}
// Определяет установлена ли переменная
function isset() {
	let args = arguments;
	let l = args.length;
	let i = 0;

	if (l === 0) {
		throw new Error('Empty isset');
	}

	while (i != l) {
		if (args[i] === undefined || args[i] === null){
			return false;
		}
		i++;
	}
	return true;
}
// Escape-ирует (заменяет мнемониками) строку
function escapeshellarg (arg) {
	let ret = '';

  	ret = arg.replace(/[^\\]'/g, function (m, i, s) {
    	return m.slice(0, 1) + '\\\\\''
  	});

  	return "'" + ret + "'";
}
//--------------------------------------------------------------------//

//  Usage
//console.log(array_diff(['Kevin', 'van', 'Zonneveld'], ['van', 'Zonneveld']));
//console.log(array_diff_assoc({0: 'Kevin', 1: 'van', 2: 'Zonneveld'}, {0: 'Kevin', 4: 'van', 5: 'Zonneveld'}));
//console.log(array_diff_key({red: 1, green: 2, blue: 3, white: 4}, {red: 5}));
//console.log(array_diff_key({red: 1, green: 2, blue: 3, white: 4}, {red: 5}, {red: 5}));
/*
let $array1 = {a: 'green', b: 'brown', c: 'blue', 0: 'red'}; 
let $array2 = {a: 'GREEN', B: 'brown', 0: 'yellow', 1: 'red'};
let res =  array_diff_uassoc($array1, $array2, function (key1, key2) { 
	return (key1 === key2 ? 0 : (key1 > key2 ? 1 : -1)) 
});
console.log(res);*/
/*
console.log(array_change_key_case(42));
console.log(array_change_key_case([ 3, 5 ]));
console.log(array_change_key_case({ FuBaR: 42 }));
console.log(array_change_key_case({ FuBaR: 42 }, 'CASE_LOWER'));
console.log(array_change_key_case({ FuBaR: 42 }, 'CASE_UPPER'));
console.log(array_change_key_case({ FuBaR: 42 }, 2));
*/
/*
console.log(array_chunk(['Kevin', 'van', 'Zonneveld'], 2));
console.log(array_chunk(['Kevin', 'van', 'Zonneveld'], 2, true));
console.log(array_chunk({1:'Kevin', 2:'van', 3:'Zonneveld'}, 2));
console.log(array_chunk({1:'Kevin', 2:'van', 3:'Zonneveld'}, 2, true));
*/
/*
console.log(array_column([{name: 'Alex', value: 1}, {name: 'Elvis', value: 2}, {name: 'Michael', value: 3}], 'name')); // {0: "Alex", 1: "Elvis", 2: "Michael"}
console.log(array_column({0: {name: 'Alex', value: 1}, 1: {name: 'Elvis', value: 2}, 2: {name: 'Michael', value: 3}}, 'name')); // {0: "Alex", 1: "Elvis", 2: "Michael"}
console.log(array_column([{name: 'Alex', value: 1}, {name: 'Elvis', value: 2}, {name: 'Michael', value: 3}], 'name', 'value')); // {0: "Alex", 1: "Elvis", 2: "Michael"}
console.log(array_column([{name: 'Alex', value: 1}, {name: 'Elvis', value: 2}, {name: 'Michael', value: 3}], null, 'value')); // 1: {name: "Alex", value: 1}
//2: {name: "Elvis", value: 2}
//3: {name: "Michael", value: 3}
*//*
console.log(array_count_values([ 3, 5, 3, "foo", "bar", "foo", "foo" ]));
console.log(array_count_values({ p1: 3, p2: 5, p3: 3, p4: "foo", p5: "bar", p6: "foo" }));
console.log(array_count_values([ true, 4.2, 42, "fubar" ]));
*//*
let $array1 = {blue: 1, red: 2, green: 3, purple: 4};
let $array2 = {green: 5, blue: 6, yellow: 7, cyan: 8};
let res = array_diff_ukey($array1, $array2, function (key1, key2){ 
	return (key1 === key2 ? 0 : (key1 > key2 ? 1 : -1)); 
});
console.log(res); // {red: 2, purple: 4}
*/
//console.log(array_fill(5, 6, 'banana'));
// { 5: 'banana', 6: 'banana', 7: 'banana', 8: 'banana', 9: 'banana', 10: 'banana' }
/*
let $keys = {'a': 'foo', 2: 5, 3: 10, 4: 'bar'};
console.log(array_fill_keys($keys, 'banana'));
// {"foo": "banana", 5: "banana", 10: "banana", "bar": "banana"}
*/
// console.log(array_flip( {a: 1, b: 1, c: 2} )); // {1: 'b', 2: 'c'}
// console.log(array_key_exists('kevin', {'kevin': 'van Zonneveld'})); // true
/*
let $arr1 = {"color": "red", 0: 2, 1: 4}; 
let $arr2 = {0: "a", 1: "b", "color": "green", "shape": "trapezoid", 2: 4};
console.log(array_merge($arr1, $arr2));

let $arr3 = [];
let $arr4 = {1: "data"};
console.log(array_merge($arr3, $arr4));
*//*
console.log(array_pad([ 7, 8, 9 ], 2, 'a')); // [ 7, 8, 9]
console.log(array_pad([ 7, 8, 9 ], 5, 'a')); // [ 7, 8, 9, 'a', 'a']
console.log(array_pad([ 7, 8, 9 ], 5, 2)); // [ 7, 8, 9, 2, 2]
console.log(array_pad([ 7, 8, 9 ], -5, 'a')); // [ 'a', 'a', 7, 8, 9 ]
*/
//console.log(isTextSymbol('-'));
//console.log(ctype_alpha('GFsadcfvrb gt'));
//console.log(isDigitSymbol('4'));
//console.log(ctype_digit('2398297832478'));
//console.log(ctype_alnum('sdsdf45'));
//console.log(bindec('110011')); // 51
//console.log(bindec('000110011')); // 51
//console.log(bindec('111')); // 7
//console.log(decbin(7));
//console.log(dechex(10)); // 'a'
//console.log(dechex(47)); // '2f'
//console.log(dechex(-1415723993)); // 'ab9dc427'
//console.log(decoct(15)); // '17'
//console.log(decoct(264)); // '410'
//console.log(hexdec('that')); // 10
//console.log(octdec('77')); // 63
//console.log(capwords('kevin van zonneveld'));
//console.log(Index('Anonynous', 'A')); // 0
/*
console.log(frexp(1)); // [0.5, 1]
console.log(frexp(1.5)); // [0.75, 1]
console.log(frexp(3 * Math.pow(2, 500))); // [0.75, 502]
console.log(frexp(-4)); // [-0.5, 3]
console.log(frexp(Number.MAX_VALUE)); // [0.9999999999999999, 1024]
console.log(frexp(Number.MIN_VALUE)); // [0.5, -1073]
console.log(frexp(-Infinity)); // [-Infinity, 0]
console.log(frexp(-0)); // [-0, 0]
console.log(frexp(NaN)); // [NaN, 0]
console.log(boolval(true)); // true
console.log(boolval(false)); // false
console.log(boolval(0));  // false
console.log(boolval(0.0));  // false
console.log(boolval(''));  // false
console.log(boolval('0'));  // false
console.log(boolval([]));  // false
console.log(boolval(null));  // false
console.log(boolval(undefined));  // false
console.log(boolval('true')); // true
*/
//console.log(doubleval('186'));
//console.log(is_bool(false)); // true
//console.log(is_bool(0)); // false
/*
console.log(is_object('23')); // false
console.log(is_object({foo: 'bar'})); // true
console.log(is_object(null)); // false
console.log(is_scalar(12));
console.log(is_unicode('We the peoples of the United Nations...!'));
*/
console.log(escapeshellarg("kevin's birthday"));
