/*
  @fileoverview MuonJS v3.2.0
  @version 3.2.0
  @license MIT
  @copyright (c) 2019 Futurum, Inc
*/
// Константы 
let Const = (function() {
	let obj = {};
	obj.PI = 3.1415926535897932385;
	obj.EPSILON = 1e-5;
	obj.E = 2.718281828459045;
	obj.TAU = 2 * this.PI;
	obj.PI_2 = 1.57079632679489661923;
	obj.PI_4 = 0.785398163397448309616;
	obj.DEG_PER_RAD = Math.PI / 180;
	obj.RAD_PER_DEG = 180 / Math.PI;
	obj._2_SQRT_PI = 1.12837916709551257390;
	obj.LOG2E = 1.44269504088896340736;
	obj.LOG10E = 0.434294481903251827651;
	obj.LN2 = 0.693147180559945309417;
	obj.LN10 = 2.30258509299404568402;
	obj.LNPI = 1.1447298858494001741;
	obj.Mills = 1.3063778838630806904686144926;
	obj.Aperi = 1.2020569031595942853997381615114;
	//Пластическое число
	obj.Plast = 1.324717957244746025960908;
	//Золотое сечение
	obj.GoldenRatio = 1.6180339887498948482;
	//Серебряное сечение
    obj.SilverRatio = 2.4142135623;
    //Бронзовое сечение
    obj.BronseRatio = 3.30277563773;
    //Сверхзолотое сечение
    obj.SuperGoldenRation = 1.46557123187676802665;
    //Постоянная Эйлера — Маскерони
    obj.EulerMascheroni = 0.5772156649015328606;
    //Постоянная Каэна
    obj.Kaen = 0.64341054629;
    //Константа Майсселя — Мертенса
    obj.MaisselMertens = 0.26149721284764278375542683860869585;
    //Предел Лапласа
    obj.LimitLaplas = 0.6627441934918158097474209710925290;
    //Константа Ландау — Рамануджана
    obj.LandauRamanudgana = 0.76422365358922066;
    //Скорость света в вакууме
    obj.SpeedOfLight = 2.99792458e+8;
    //Гравитационная постоянная
    obj.GravitationalConstant = 6.67429e-11;
    //Постоянная Планка
    obj.PlancksConstant = 6.62606896e-34;
    // Элементарный заряд
    obj.ElementaryCharge = 1.602176487e-19;
    //Электрон
    obj.ElectronMass = 9.10938215e-31;
    // Мюон
    obj.MuonMass = 1.88353130e-28;
    // Тау
    obj.TauMass = 3.16777e-27;
    //Протон
    obj.ProtonMass = 1.672621637e-27;
    //Нейтрон
    obj.NeutronMass = 1.674927212e-27;
    //Дейтрон
    obj.DeuteronMass = 3.34358320e-27;
    // Гелион
    obj.HelionMass = 5.00641192e-27;
	return obj;
})();

let echo = (text) => { document.body.innerHTML = text; };
let cl = (text) => { console.log(text); };
let al = (text) => { alert(text); }
let noop = () => undefined;
let voidf = () => void 0;
// Удаление незначащих пробелов в начале и конце строки.
// @return { string }
function trim(string) {
    return string.replace(/^\s*/, "").replace(/\s*$/, "");
}
// Удаление незначащих пробелов в начале строки.
// @return { string }
function ltrim(string) {
    return string.replace(/^\s*/, "");
}
// Удаление незначащих пробелов в конце строки.
// @return { string }
function rtrim(string) {
    return string.replace(/\s*$/, "");
}
// Функция, заменяющая eval
// @return { undefined }
function execscr(code) {
    let el = document.createElement('script');
    el.setAttribute('type', 'text/javascript');
    el.appendChild(document.createTextNode(code));
    document.body.appendChild(el);
    return el;
}
// Проверяет активна ли текущая вкладка
// @return { boolean }
function pageIsVisible() {
    return (document.visibilityState || document.mozVisibilityState ||
    	document.msVisibilityState || document.webkitVisibilityState) == 'visible';
}
// Проверяет с какой страницы пришел пользователь
function referrer() {
	console.log(document.referrer);
}

// Проверяет, содержится ли данное значение в массиве
// @return { boolean }
Array.prototype.inArray = function (value) {
    for (let i=0; i < this.length; i++) {
        if (this[i] === value) {
            return true;
        }
    }
    return false;
};
// Очищает полностью содержимое указанного элемента
function clearElement(elem) {
  /*while (elem.firstChild) {
        elem.firstChild.remove();
  }*/
  elem.innerHTML = '';
}

let min = Math.min;
let max = Math.max;

let Math_ = (function() {
	return {
		sqr: function(number){
			return number * number;
		},
		pow(n, m){
			return Math.round(Math.exp(Math.log(n) * m));
		},
		tqrt(number){
			return this.pow(number, 1/4);
		},
		nqrt(number, n){
			return this.pow(number, 1/n);
		},
		// Возвращает целую часть числа
		int(number){ 
			// 1- й способ:
			// return number - (number%1);
			// 2-й способ:
			// return number << 0; 
			// 3-й способ:
			// return ~~number; 
			// 4-й способ:
			// return number ^ 0;
			let result = (number >= 0) ? Math.floor(number) : Math.ceil(number);
			return result;
		},
		// Возвращает дробную часть числа
		frac(number){
			// 1-й способ:
			//return Math.round((number - parseInt(number)) * 10);
            let str = number.toString();
            let decimalOnly = 0;

            if( str.indexOf('.') != -1){
            	decimalOnly = parseFloat(Math.abs(number).toString().split('.')[1]);
            }
            return decimalOnly;
		},
		// Является ли число составным
		isComposite: function(number){
			if ((number % 2 == 0) && (number != 2)){
				return true;
			} else {
				return false;
			}
		},
		// Является ли число простым
		isPrime: function(number){
			let k = Math.round(Math.sqrt(number));

			if (number == 2){ return true;}

			for(let i = 2; i < k+1 ; i++){
				if (number % i == 0){
					return true;
					return;
				}
				return false;
			}
		},
		bin: (number) => Number.parseInt(number.toString(2)),
		oct: (number) => Number.parseInt(number.toString(8)),
		hex: (number) => number.toString(16).toUpperCase(),
		// Является ли значение числом
		isNumeric: function(variable){
			// 1-й способ:
			// return !isNaN(parseFloat(variable)) && isFinite(parseFloat(variable));
			return Number.isInteger(variable);
		},
		// Является ли число четным
		isEven: function(number) {
			if (Number.isInteger(number)){
				return (number % 2 == 0)? true : false;
			}
		},
		// Возвращает кортеж(массив)из 2 чисел, где 1-е -дробная часть, 2-е - целая часть числа number
		modf: function(number) {
			return [this.frac(number), this.int(number)];
		},
		// Возвращает точную сумму значений в итерируемом объекте
		fsum: function(...args){
			let sum = args.reduce( function(total, item) {
				return total + item;
			}, 0);
			return sum;
		},
		// Переводит радианы в градусы Цельсия
		degrees: function(rad){
			return rad * RAD_PER_DEG;
		},
		// Переводит градусы Цельсия в радианы
		radians: function(deg){
			return deg * DEG_PER_RAD;
		},
		signbit: function(x) {
			return (x = +x) != x ? x : x == 0 ? 1 / x == Infinity : x > 0;
		},
		clamp: function(x, lower, upper) {
			return min(upper, max(lower, x));
		},
		fscale: function(x, inLow, inHigh, outLow, outHigh) {
			 return Math.fround(Math.scale(x, inLow, inHigh, outLow, outHigh));
		},
		// Максимальная цифра числа
        maxDigit: function(number) {
			if (number < 10) return number;
			let l = number % 10;
			let m = this.maxDigit(parseInt(number / 10));
			if (m > l) return m;
			else return l;
		},
		// Добавление цифр в конец числа
		addAfter: function(number, after) {
			return number * 10 + after;
		}
	}
})();

let Random = (function() {
	return {
		// Случайное дробное число между min и max
		// @return { number (float) }
		randomFrac: function(min, max){
			return Math.random() * (max - min) + min;
		},
		// Случайное целое между min и max
		// @return { number (int) }
		randomInt: function(min, max){
			return Math.floor(Math.random() * (max - min + 1)) + min;
		}
	}
})();

// Реализует множественное наследование объектов
// @return { void 0 }
function mixin(obj, ...args) {
    args.forEach(function(item, i) {
		Object.assign(obj, item);
	});
}	

(function (){
  
  // Представляет двумерную точку		
  Point2 = function(x, y) {
  	this.X = x;
  	this.Y = y;

  	this.DistanceTo = function(point){
		return Math.sqrt(Math_.sqr(this.X - point.X) + Math_.sqr(this.Y - point.Y));
	}
	this.GetRadiusVectorLength = function(){
		return Math.sqrt( this.X * this.X + this.Y * this.Y);
	}

	this.Clone = function(){ return new Point2(this.X, this.Y);}
    this.Equals = function(object){
    	if(object instanceof Point2){
    		return (this.X == object.X && this.Y == object.Y);
    	}else{
    		return false;
    	}
    }

  	this.toString = function(){
  	 	return `[${this.X}, ${this.Y}]`;
  	}
  }

  Vector2 = function(x, y){
	if(x === undefined && y === undefined){
		return;
	} else if (Number.isInteger(x) &&  y === undefined){
		this.Y = this.X = x;
	} else {
		this.X = x;
		this.Y = y;
	}
	//Возвращает вектор противоположный данному
	this.Negate = function(){
		return new Vector2(-this.X, -this.Y);
	}

	this.Abs = function() {
		if ((this.X != undefined) && (this.Y != undefined)){
			return new Vector2(Math.abs(this.X), Math.abs(this.Y));
		} else {
			return undefined;
		}
	}

	this.Sqrt = function() {
		if ((this.X >= 0) && (this.Y >= 0)){
			return new Vector2(Math.sqrt(this.X), Math.sqrt(this.Y));
		} else {
			return undefined;
		}
	}

	this.Dot = function(v1, v2) {
		if ((typeof v1 === 'object') && (typeof v2 === 'object')){
			return v1.X * v2.X + v1.Y * v2.Y;
		} else {
			throw new Error('ArgumentNullException: Type mismatch!'); // Hесоответствие типов
		}
	}

	this.Add = function(v1, v2) {
		return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
	}

	this.Sub = function(v1, v2) {
		return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
	}

	this.Mul = function(v1, v2) {
		if (typeof v2 === 'object'){
			return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
		} else if ((typeof v2 === 'number') || (Number.isInteger(v2))){
			return new Vector2(v1.X * v2, v1.Y * v2);
		}
	}

	this.Div = function(v1, v2) {
		if (typeof v2 === 'object'){
			if ((v2.X != 0) && (v2.Y != 0)){
				return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
			} else {
				throw new Error('DivideByZeroException: Divide 0!');
			}
		} else if ((typeof v2 === 'number') || (Number.isInteger(v2))){
			return new Vector2(v1.X / v2, v1.Y / v2);
		}	
	}

	this.Clone = function(){
		return new Vector2(this.X, this.Y);
	}
	//Возвращает массив элементов вектора 
	this.CopyTo = function(){
		return [this.X, this.Y];
	}
	// @return { boolean }
	this.Equals = function(object){
		if (object instanceof Vector2){
			return ((object.X == this.X) && (object.Y == this.Y))? true : false;
		} else {
			return false;
		}
	}
	// Преобразовывает строку к Vector2 если это возможно
	// @return { Vector2 }
	this.Parse = function(str){
		if (typeof str === 'string'){
			if ((str.length == 0) || str == null){
				throw new TypeError('TypeError: Строка имела неверный формат!');
			} else {
				let vals = str.split(' ');
				if (vals.length != 2){
					throw new TypeError('TypeError: Строка имела неверный формат!');
				}

				try{
					this.X = parseFloat(vals[0]);
					this.Y = parseFloat(vals[1]);
				} catch(ex){
					throw new TypeError('TypeError: ' + ex.Message);
				}
				return this;
			}	
		} else {
			return;
		}
	}

	this.toString = function() {
		return `[${this.X} : ${this.Y}]`;
	}
}

Vector2.prototype.Zero = () => new Vector2(0,0);
Vector2.prototype.One = () => new Vector2(1,1);
Vector2.prototype.UnitX = () => new Vector2(1,0);
Vector2.prototype.UnitY = () => new Vector2(0,1);
Vector2.prototype.Right = () => new Vector2(1,0);
Vector2.prototype.Left = () => new Vector2(-1,0);
Vector2.prototype.Backward = () => new Vector2(0,1);
Vector2.prototype.Forward = () => new Vector2(0,-1);

// Статический метод 
Vector2.Distance = function(vector1, vector2){
	let num1 = vector1.X - vector2.X;
	let num2 = vector1.Y - vector2.Y;
	return Math.sqrt((num1 ** 2) + (num2 ** 2));
};

Vector2.prototype.Length = function(){
		return Math.sqrt((this.X ** 2) + (this.Y ** 2));
};

Vector2.prototype.Normalize = function(){
		let length = this.Length();
		this.X /= length;
		this.Y /= length;
		return new Vector2(this.X, this.Y);
};

// Представляет комплексное число
Complex = function(real, imaginary){
	if(real === undefined && imaginary === undefined){
		return;
	} else if (real !== undefined &&  imaginary === undefined){
		this.imaginary = this.real = real;
	} else {
		this.real = real;
		this.imaginary = imaginary;
	}

    this.Negate = function(){
    	return new Complex(-this.real, -this.imaginary);
    }

    this.Add = function(complex1, complex2){
    	return new Complex(complex1.real + complex2.real, complex1.imaginary + complex2.imaginary);
    }

    this.Sub = function(complex1, complex2){
    	return new Complex(complex1.real - complex2.real, complex1.imaginary - complex2.imaginary);
    }

    this.Mul = function(complex1, complex2){
    	if (typeof complex2 === 'object'){
    		return new Complex(complex1.real * complex2.real, complex1.imaginary * complex2.imaginary);
    	} else if ((typeof complex2 === 'number') || (Number.isInteger(complex2))){
    		return new Complex(complex1.real * complex2, complex1.imaginary * complex2);
    	}
    }

    this.Div = function(complex1, complex2) {
		if ((typeof complex2 === 'number') || (Number.isInteger(complex2))){
			return new Complex(complex1.real / complex2, complex1.imaginary / complex2);
		}	
	}

	this.Clone = function(){
		return new Complex(this.real, this.imaginary);
	}
	//Возвращает массив элементов
	this.CopyTo = function(){
		return [this.real, this.imaginary];
	}

	this.toString = function(){
		return `[${this.real} + ${this.imaginary}i]`;
	}
	// // @return { boolean }
	this.Equals = function(object){
		if (object instanceof Complex){
			return ((object.real == this.real) && (object.imaginary == this.imaginary))? true : false;
		} else {
			return false;
		}
	}
	//Преобразовывает строку к Complex если это возможно
	// @return { Complex }
	this.Parse = function(str){
		if (typeof str === 'string'){
			if ((str.length == 0) || str == null){
				throw new TypeError('TypeError: Строка имела неверный формат!');
			} else {
				let vals = str.split(' ');
				if (vals.length != 2){
					throw new TypeError('TypeError: Строка имела неверный формат!');
				}

				try{
					this.real = parseFloat(vals[0]);
					this.imaginary = parseFloat(vals[1]);
				} catch(ex){
					throw new TypeError('TypeError: ' + ex.Message);
				}
				return this;
			}	
		} else {
			return;
		}
	}
}

Complex.prototype.Zero = function(){
	return new Complex(0,0);
};

Complex.prototype.One = function(){
	return new Complex(1,1);
};

Complex.prototype.ImaginaryOne = function(){
	return new Complex(0,1);
};

Complex.prototype.Conjugate = function(){
	return new Complex(this.real, -this.imaginary);
};

// Представляет кватернион
Quaternion = function(x, y, z, w){
	if((x === undefined) && (y === undefined) && (z === undefined) && (w === undefined)){
		return;
	} else if ((x !== undefined) &&  (y === undefined) &&  (z === undefined) &&  (w === undefined)){
		this.W = this.Z = this.Y = this.X = x;
	} else {
		this.X = x;
		this.Y = y;
		this.Z = z;
		this.W = w;
	}

	this.Negate = function(){
		return new Quaternion(-this.X, -this.Y, -this.Z, -this.W);
	}

	this.Dot = function(q1, q2) {
		return q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;
	}

	this.Add = function(v1, v2){
		return new Quaternion(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
	}

    this.Sub = function(v1, v2){
    	return new Quaternion(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }
    // @return { boolean }
    this.Equals = function(object){
		if (object instanceof Quaternion){
			return ((object.X == this.X) && (object.Y == this.Y) && (object.Z == this.Z) && (object.W == this.W))? true : false;
		} else {
			return false;
		}
	}
	// Преобразовывает строку к Quaternion если это возможно
	// @return { Quaternion }
	this.Parse = function(str){
		if (typeof str === 'string'){
			if ((str.length == 0) || str == null){
				throw new TypeError('TypeError: Строка имела неверный формат!');
			} else {
				let vals = str.split(' ');
				if (vals.length != 4){
					throw new TypeError('TypeError: Строка имела неверный формат!');
				}

				try{
					this.X = parseFloat(vals[0]);
					this.Y = parseFloat(vals[1]);
					this.Z = parseFloat(vals[2]);
					this.W = parseFloat(vals[3]);
				} catch(ex){
					throw new TypeError('TypeError: ' + ex.Message);
				}
				return this;
			}	
		} else {
			return;
		}
	}

	this.toString = function(){
		return `${this.X} + ${this.Y}i + ${this.Z}j + ${this.W}k`;
    }
}

Quaternion.prototype.Zero = function(){
	return new Quaternion(0.0, 0.0, 0.0, 0.0);
};

Quaternion.prototype.Identity = function(){
	return new Quaternion(0.0, 0.0, 0.0, 1.0);
};

Quaternion.prototype.Length = function(){
	return Math.sqrt(this.X ** 2 + this.Y ** 2  + this.Z ** 2  + this.W ** 2 );
};

Quaternion.prototype.Conjugate = function(){
	return new Quaternion(-this.X, -this.Y, -this.Z, this.W);
};

 // Дуальное число(комплексное число параболического типа)
	Dual = function(x, y) {
		if ((x === undefined) && (y === undefined)){
			throw new Error('ArgumentNullException: Type mismatch!');
		} else if ((x !== undefined) && (y === undefined)){
			this.X = this.Y = x;
		} else {
			this.X = x;
 			this.Y = y;
		}

		this.Clone = function() {
			return new Dual(this.X, this.Y);
		}

 		this.toString = function() {
 			return `${this.X} + ${this.Y}E`;
 		}
	}
// @return { Dual }
Dual.Negate = function(dual) {
	return new Dual(-dual.X, -dual.Y);
};
// @return { Dual }
Dual.Add = function(dual1, dual2) {
 	return new Dual(dual1.X + dual2.X, dual1.Y + dual2.Y);
};
// @return { Dual }
Dual.Sub = function(dual1, dual2) {
	return new Dual(dual1.X - dual2.X, dual1.Y - dual2.Y);
}
// @return { Dual }
Dual.Mul = function(dual1, dual2) {
 	return new Dual(dual1.X * dual2.X, dual1.Y * dual2.X + dual1.X * dual2.Y);
};
// @return { Dual }
Dual.Div = function(dual1, dual2) {
 	return new Dual(dual1.X / dual2.X, (dual1.Y * dual2.X - dual1.X * dual2.Y) / Math.pow(dual2.X, 2));
};	
//Преобразовывает строку к Dual если это возможно
// @return { Dual }
Dual.Parse = function(str){
	if (typeof str === 'string'){
		if ((str.length == 0) || str == null){
			throw new TypeError('TypeError: Строка имела неверный формат!');
		} else {
			let vals = str.split(' ');
			if (vals.length != 2){
				throw new TypeError('TypeError: Строка имела неверный формат!');
			}

			try{
				this.X = parseFloat(vals[0]);
				this.Y = parseFloat(vals[1]);
			} catch(ex){
				throw new TypeError('TypeError: ' + ex.Message);
			}
			return new Dual(this.X, this.Y);
		}	
	} else {
		return;
	}
};

}());

(function(){
	TokenClass = function(){
		// Является ли символ числом
		// @return { boolean }
		this.isDigit = function(current_char){
			return /[0-9]/i.test(current_char);
		}
		// Является ли символ оператором
		// @return { boolean }
		this.isOperator = function(current_char){
			return "+-*/%=&|<>!~".indexOf(current_char) >= 0
		}
		// Является ли символ пунктуационным
		// @return { boolean }
		this.isPunc = function(current_char){
			return ",;(){}[]".indexOf(current_char) >= 0;
		}
		// Является ли символ пробельным
		// @return { boolean }
		this.isWhiteSpace = function(current_char){
			return " \t\n".indexOf(current_char) >= 0;
		}
	}
})();


// Альтернатива циклу for..of. Удобна для отладки
// Проходит по коллекции и выводит элементы в консоль
// @param collection - коллекция(объект) или строка
// @return { undefined }
function forof (collection) {
	if (typeof collection === 'object') {
		let items = collection.entries();
	    let res = items.next();

	    while (res.done === false) {
		    console.log(res.value[0], res.value[1]);
		    res = items.next();
	    }
	} else if (typeof collection === 'string') {
		let iterator = collection[Symbol.iterator]();

		while(true) {
			let res = iterator.next();
			if (res.done) break;
			console.log(res.value);
		}
	}	
}
// Итерирует числа от from до to  и выводит их на консоль
// @param from - начальное значение 
// @param to - конечное значение при котором done === true
// @return { undefined }
function __iter__ (from, to) {
	let range = {
		from, to
	};

	range[Symbol.iterator] = function() {
		let current = this.from;
		let last = this.to;

		return {
			next() {
				if (current <= last) {
					return {
						done: false,
						value: current++
					};
				} else {
					return {
						done: true
					};
				}
			}
		};
	}

	for (let number of range) {
		console.log(number);
	}
}

// Является ли символ буквой
// @return { boolean }
function isTextSymbol(current_symbol) {
	return /[A-Za-z]/i.test(current_symbol);
}
// Является ли символ цифрой
// @return { boolean }
function isDigitSymbol(current_symbol) {
	return /[0-9]/i.test(current_symbol);
}
// Содержит ли указанная строка только буквы
// @return { boolean }
function ctype_alpha(text) {
	if (typeof text !== 'string'){
		return false;
	}

	let arr = Array.from(text);
	arr.forEach( function(elem, index) {
		if (!isTextSymbol(elem)){
			return false;
		}
	});
	return true;
}
// Содержит ли указанная строка только цифры
// @return { boolean }
function ctype_digit(text) {
	if (typeof text !== 'string'){
		return false;
	}

	let arr = Array.from(text);
	arr.forEach( function(elem, index) {
		if (!isDigitSymbol(elem)){
			return false;
		}
	});
	return true;
}
// Содержит ли указанная строка алфавитный символ
function ctype_alnum (text) {
	return (ctype_alpha(text) || ctype_digit(digit));
}
// Является ли символ управляющим( Управляющие символы это \n, \t, esc, и т.д.)
function isControlSymbol (current_symbol) {
	return /(\t|\n)+/gim.test(current_symbol);
}
// Возвращает true если символ в нижнем регистре, иначе false
function isLowerSymbol (current_symbol) {
	if (current_symbol === current_symbol.toLowerCase()) return true;
	else return false;
}
// Возвращает true если символ в верхнем регистре, иначе false
function isUpperSymbol (current_symbol) {
	if (current_symbol === current_symbol.toUpperCase()) return true;
	else return false;
}
//--------------- Математические функции -------------------------//
// Конвертирует двоичное число в integer
// @return { int }
function bindec (binaryString) {
	binaryString = (binaryString + '').replace(/[^01]/gi, '')
	return parseInt(binaryString, 2);
}
// Возвращает строку - двоичное представление данного аргумента number
// @return { string }
function decbin (number) {
	if (number < 0) {
		number = 0xFFFFFFFF + number + 1;
	}
	return parseInt(number, 10).toString(2);
}
// Возвращает строку - шестнадцатеричное представление данного аргумента number
// @return { string }
function dechex (number) {
	if (number < 0) {
    	number = 0xFFFFFFFF + number + 1
  	}
  	return parseInt(number, 10).toString(16);
}
// Возвращает строку - восьмеричное представление данного аргумента number
// @return { string }
function decoct (number) {
	if (number < 0) {
    	number = 0xFFFFFFFF + number + 1
  	}
  	return parseInt(number, 10).toString(8);
}
// Вовзращает максимально возможное случайное число
function getrandmax() {
	return 2147483647;
}
// Возвращает десятеричный эквивалент 16-ричного числа, заданного аргументом hexString
// @return { int }
function hexdec (hexString) {
	hexString = (hexString + '').replace(/[^a-f0-9]/gi, '');
	return parseInt(hexString, 16);
}
// Возвращает десятеричный эквивалент 8-ричного числа, заданного аргументом octString
// return { int }
function octdec (octString) {
	octString = (octString + '').replace(/[^0-7]/gi, '');
	return parseInt(octString, 8);
}
// Возвращает true, если value является допустимым конечным числом в пределах диапазона чисел с плавающей точкой
// @return { boolean }
function is_finite(value) {
	let warningType = '';
	if (value === Infinity || value === -Infinity) {
		return false;
	}

	if (typeof value === 'object') {
		warningType = (Object.prototype.toString.call(value) === '[object Array]'? 'array': 'object');
    } else if (typeof value === 'string' && !value.match(/^[+-]?\d/)) {
    	warningType = 'string';
    }
    if (warningType) {
    	let msg = 'Warning: is_finite() expects parameter 1 to be double, ' + warningType + ' given';
    	throw new Error(msg);
    }
    return true;
}
// Возвращает true, если value является бесконечным (положительным или отрицательным)
// @return { boolean }
function is_infinite(value) {
	return !is_finite(value);
}
// @return { number }
function hypot(x, y) {
	x = Math.abs(x);
	y = Math.abs(y);

	return Math.sqrt(x ** 2 + y ** 2);
}
// Константное перечисление букв из набора ASCII в верхнем и нижнем регистрах
// Добавлена из Python
// @return { string }
function ascii_letters() {
	const length = 26;
	let i = 65;
	return [...Array(length + 6 + length)].reduce(function (accumulator) {
      	return accumulator + String.fromCharCode(i++)
    }, '').match(/[a-zA-Z]+/g).reverse().join('')
}
// Константное перечисление букв из набора ASCII в нижнем регистре
// Добавлена из Python
// @return { string }
function ascii_lowercase() {
	const length = 26;
	let i = 65 + length + 6;

	return [...Array(length)].reduce( function(accumulator) {
		return accumulator + String.fromCharCode(i++);
	}, '');
}
// Константное перечисление букв из набора ASCII в верхнем регистре
// Добавлена из Python
// @return { string }
function ascii_uppercase() {
	const length = 26;
	let i = 65;

	return [...Array(length)].reduce( function(accumulator) {
		return accumulator + String.fromCharCode(i++);
	}, '');
}
// Kапитализировать все слова в строке
// Добавлена из Python
// @return { string }
function capwords(str) {
	let pattern = /^([a-z\u00E0-\u00FC])|\s+([a-z\u00E0-\u00FC])/g
  	return (str + '').replace(pattern, function ($1) {
    	return $1.toUpperCase()
  	});
}
//-----------------------------------------------------------------------------//
// Работа с элементами DOM
// Работать одинаково при передаче DOM-узла или его id.
// // @return { ELEMENT_NODE }
function byId(node) {
        return typeof node == 'string' ? document.getElementById(node) : node
}
// Скрывает элемент
// @return { void 0 }
function hide(node) {
	// node = byId(node);
	node.style.display = 'none';
}
// Меняет цвет текста для элемента
// @return { void 0 }
function chgcolor (node, color) {
	node.style.color = color;
}
// Меняет цвет фона для элемента
// @return { void 0 }
function chgbground (node, color) {
	node.style.background = color;
}
// Меняет оступы для элемента
// @return { void 0 }
function chgpadd (node, padding) {
	node.style.padding = padding;
}

function setHeightByWidthProportion (selector, proportion) {
	let element = document.querySelector(selector);
    let v = element.offsetWidth * proportion;
    element.style.height = v + 'px';
}
//---------------------------------------------
// Повторяет строку указанное кол-во раз
// @return { string }
function repString (str, num) {
	let out = '';
	for (let i = 0; i < num; i++) {
		out += str;
	}
	return out;
}
// Выводит информацию о типе переменной
// howDisplay - вид отображения информации
// 1) alert - вывод с помощью функции alert()
// 2) body - вывод на страницу
// 3) default - по умолчанию (не выводит)
// @if howDisplay == 'default' @return { string }
// @else @return { undefined }
function var_dump (value, howDisplay = 'default') {
	let ret = '';

	if (value === null) {
		ret = 'NULL';
	} else if (typeof value === 'boolean') {
		ret = 'bool(' + value + ')';
	} else if (typeof value === 'string') {
		ret = 'string(' + value.length + ') "' + value + '"';
	} else if (typeof value === 'number') {
		if (parseFloat(value) === parseInt(value, 10)) {
			ret = 'int(' + value + ')';
		} else {
			ret = 'float(' + value + ')';
		}
	} else if (typeof value === 'undefined') {
		ret = 'undefined';
	} else if (Object.prototype.toString.call(value) === '[object Array]') {
		ret = 'Array(' + value + ')';
	} else if (typeof value === 'function') {
		ret = value;
	} else if (value instanceof Date) {
		ret = 'Date(' + value + ')';
	} else if (value instanceof RegExp) {
		ret = 'RegExp(' + value + ')';
	} else if (value.nodeName) {
		switch (value.nodeType) {
			case 1:
			    if (typeof value.namespaceURI === 'undefined' ||
            	  value.namespaceURI === 'https://www.w3.org/1999/xhtml') {
			    	ret = 'HTMLElement("' + value.nodeName + '")';
			    } else {
			    	ret = 'XML Element("' + value.nodeName + '")';
			    }
			    break;
			case 2:
          		ret = 'ATTRIBUTE_NODE(' + value.nodeName + ')';
          		break;
        	case 3:
          		ret = 'TEXT_NODE(' + value.nodeValue + ')';
          		break;
        	case 4:
          		ret = 'CDATA_SECTION_NODE(' + value.nodeValue + ')';
          		break;
        	case 5:
          		ret = 'ENTITY_REFERENCE_NODE';
          		break;
        	case 6:
          		ret = 'ENTITY_NODE';
          		break;
        	case 7:
          		ret = 'PROCESSING_INSTRUCTION_NODE(' + value.nodeName + ':' + value.nodeValue + ')';
          		break;
        	case 8:
          		ret = 'COMMENT_NODE(' + value.nodeValue + ')';
          		break;
        	case 9:
          		ret = 'DOCUMENT_NODE';
          		break;
        	case 10:
          		ret = 'DOCUMENT_TYPE_NODE';
          		break;
        	case 11:
          		ret = 'DOCUMENT_FRAGMENT_NODE';
          		break;
        	case 12:
          		ret = 'NOTATION_NODE';
          		break;
		}
	} else if (typeof value === 'object' && value != null) {
		for (let entry of Object.entries(value)) {
    		ret += '\t[' + entry + ']\n';
		}

		 ret = `Object(\n` + ret + `)`;
	} 

	if (howDisplay == 'default') {
		return ret;
	} else if (howDisplay == 'alert') {
		alert(ret);
	} else if (howDisplay == 'body') {
		let pre = document.createElement('pre');
		pre.innerHTML = ret;
		pre.style.color = 'lime';
		document.body.appendChild(pre);
	}
}
// Рекурсивная функция var_dump()
// @return { string }
function var_dump_rec(arr,level) {
	let dumped_text = "";
	if(!level) level = 0;

	let level_padding = "";
	for(let j = 0; j < level+1; j++) level_padding += "    ";

	if(typeof(arr) == 'object') {
		for(let item in arr) {
			let value = arr[item];

			if(typeof(value) == 'object') { 
				dumped_text += level_padding + "'" + item + "'\n";
				dumped_text += var_dump_rec(value,level+1);
			} else {
				dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
			}
		}
	} 
	return dumped_text;
}
// Останавливает выполнение программы (аналог функции die() из языка PHP)
// @return { undefined }
function die (status) {
	let i;

	if (typeof status === 'string') {
		alert(status);
	}

	window.addEventListener('error', function (event) {
		event.preventDefault();
		event.stopPropagation();
	}, false);

	let handlers = [
		'copy', 'cut', 'paste','beforeunload', 'blur', 'change', 'click', 'contextmenu', 'dblclick', 'focus',
		'keydown', 'keypress', 'keyup', 'mousedown', 'mousemove', 'mouseout', 'mouseover', 'mouseup', 'resize', 
	    'scroll','DOMNodeInserted', 'DOMNodeRemoved', 'DOMNodeRemovedFromDocument', 
        'DOMNodeInsertedIntoDocument', 'DOMAttrModified', 'DOMCharacterDataModified', 
        'DOMElementNameChanged', 'DOMAttributeNameChanged', 'DOMActivate', 'DOMFocusIn', 'DOMFocusOut', 
        'online', 'offline', 'textInput','abort', 'close', 'dragdrop', 'load', 'paint', 'reset',
        'select', 'submit', 'unload'
	];

	function stopPropagation (event) {
        event.stopPropagation();
        // e.preventDefault();
    }

    for (i=0; i < handlers.length; i++) {
        window.addEventListener(handlers[i], function (event) {
        	 stopPropagation(event);
        }, true);
    }

    if (window.stop) {
        window.stop();
    }

    throw '';
}

// Нельзя забывать о огромной армии пользователей старых браузеров, 
// поэтому напишем функции, которые позволят кросс-браузерно выполнять поиск дочерних элементов

// Поддерживает ли браузер интерфейс "Element Traversal"?
let traversal = typeof (document.createElement('div').childElementCount) != undefined;
// Ищет первый элемент-потомок в узле node
// @param node - родительский узел
// @return { ELEMENT_NODE }
let firstChild = traversal ? function(node) {
	// для новых браузеров пользуемся встроенным методом
	return node.firstElementChild;
} : function(node) {
	// для старых браузеров
	node = node.firstChild;
	// ищем следующий узел пока nodeType == 1
	while (node && node.nodeType != 1) node = node.nextSibling;
	return node;
};
// Ищет последний элемент-потомок в узле node
// @param node - родительский узел
// @return { ELEMENT_NODE }
let lastChild = traversal ? function(node) {
	return node.lastElementChild;
} : function(node) {
	node = node.lastChild;
	while (node && node.nodeType != 1) node = node.previousSibling;
	return node;
};
// Ищет следующий элемент в узле node
// @param node - родительский узел
// @return { ELEMENT_NODE }
let next = traversal ? function(node) {
	return node.nextElementSibling;
} : function(node) {
	while (node = node.nextSibling) if (node.nodeType == 1) break;
	return node;
};
// Ищет предыдущий элемент в узле node
// @param node - родительский узел
// @return { ELEMENT_NODE }
let previous = traversal ? function(node) {
	return node.previousElementSibling;
} : function(node) {
	while (node = node.previousSibling) if (node.nodeType == 1) break;
	return node;
};

// Поддерживает ли браузер метод children?
let children = typeof (document.createElement('div').children) != undefined;

// Возвращает коллекцию дочерних узлов
// @param node - родительский узел
// @return { ELEMENT_NODE_COLLECTION }
let childs = children ? function(node) {
	return node.children;
} : function(node) {
	let list = node.childNodes,
    	length = list.length,
    	i = -1,
    	array = [];

    while(++i < length)
        if(list[i].nodeType == 1) array.push(list[i]);

    return array;
};
// Запрещает открывать контекстное меню
// @return { boolean }
let banHtml = function() {
	document.oncontextmenu = function(event) {
		return false;
	};
}
// Отменяет запрет открывать контекстное меню
// @return { boolean }
let cancelbanHtml = function() {
	document.oncontextmenu = function(event) {
		return true;
	};
}
// Функция-переключатель
// @return { void 0 }
function toggle (object) {
	let element = document.getElementById(object);
	if (element.style.display != 'none') {
		element.style.display = 'none';
	} else {
		element.style.display = '';
	}
}
// Есть небольшая проблема при определении целевого элемента у события. 
// Вместо вызова e.target в Internet Explorer необходимо использовать e.srcElement. 
// Самым простым решением для устранения этой проблемы является небольшая функция getEventTarget
function getEventTarget(event) {
	let e = event | window.event;
	let target = e.target || e.srcElement;
	if (target.nodeType == 3) {
		target = target.parentNode;
	}
	return target;
}
// Преобразует специальные символы (например, >) в их закодированные значения (например, &gt;)
// Является аналогом функции htmlEntities() из языка PHP 
// @return { string }
function htmlEntities (string) {
	return String(string)
		.replace(/&/g, '&amp;')
		.replace(/</g, '&lt;')
		.replace(/>/g, '&gt;')
		.replace(/"/g, '&quot;');
}
// Возвращает HEX-код цвета по его имени
// @param { name - название цвета }
// @return { string }
function ColorToHex(name){
	return {
		"aliceblue":"#f0f8ff",
		"antiquewhite":"#faebd7",
		"aqua":"#00ffff",
		"aquamarine":"#7fffd4",
		"azure":"#f0ffff",
		"beige":"#f5f5dc",
		"bisque":"#ffe4c4",
		"black":"#000000",
		"blanchedalmond":"#ffebcd",
		"blue":"#0000ff",
		"blueviolet":"#8a2be2",
		"brown":"#a52a2a",
		"burlywood":"#deb887",
		"cadetblue":"#5f9ea0",
		"chartreuse":"#7fff00",
		"chocolate":"#d2691e",
		"coral":"#ff7f50",
		"cornflowerblue":"#6495ed",
		"cornsilk":"#fff8dc",
		"crimson":"#dc143c",
		"cyan":"#00ffff",
		"darkblue":"#00008b",
		"darkcyan":"#008b8b",
		"darkgoldenrod":"#b8860b",
		"darkgray":"#a9a9a9",
		"darkgreen":"#006400",
		"darkkhaki":"#bdb76b",
		"darkmagenta":"#8b008b",
		"darkolivegreen":"#556b2f",
		"darkorange":"#ff8c00",
		"darkorchid":"#9932cc",
		"darkred":"#8b0000",
		"darksalmon":"#e9967a",
		"darkseagreen":"#8fbc8f",
		"darkslateblue":"#483d8b",
		"darkslategray":"#2f4f4f",
		"darkturquoise":"#00ced1",
		"darkviolet":"#9400d3",
		"deeppink":"#ff1493",
		"deepskyblue":"#00bfff",
		"dimgray":"#696969",
		"dodgerblue":"#1e90ff",
		"feldspar":"#d19275",
		"firebrick":"#b22222",
		"floralwhite":"#fffaf0",
		"forestgreen":"#228b22",
		"fuchsia":"#ff00ff",
		"gainsboro":"#dcdcdc",
		"ghostwhite":"#f8f8ff",
		"gold":"#ffd700",
		"goldenrod":"#daa520",
		"gray":"#808080",
		"green":"#008000",
		"greenyellow":"#adff2f",
		"honeydew":"#f0fff0",
		"hotpink":"#ff69b4",
		"indianred":"#cd5c5c",
		"indigo":"#4b0082",
		"ivory":"#fffff0",
		"khaki":"#f0e68c",
		"lavender":"#e6e6fa",
		"lavenderblush":"#fff0f5",
		"lawngreen":"#7cfc00",
		"lemonchiffon":"#fffacd",
		"lightblue":"#add8e6",
		"lightcoral":"#f08080",
		"lightcyan":"#e0ffff",
		"lightgoldenrodyellow":"#fafad2",
		"lightgrey":"#d3d3d3",
		"lightgreen":"#90ee90",
		"lightpink":"#ffb6c1",
		"lightsalmon":"#ffa07a",
		"lightseagreen":"#20b2aa",
		"lightskyblue":"#87cefa",
		"lightslateblue":"#8470ff",
		"lightslategray":"#778899",
		"lightsteelblue":"#b0c4de",
		"lightyellow":"#ffffe0",
		"lime":"#00ff00",
		"limegreen":"#32cd32",
		"linen":"#faf0e6",
		"magenta":"#ff00ff",
		"maroon":"#800000",
		"mediumaquamarine":"#66cdaa",
		"mediumblue":"#0000cd",
		"mediumorchid":"#ba55d3",
		"mediumpurple":"#9370d8",
		"mediumseagreen":"#3cb371",
		"mediumslateblue":"#7b68ee",
		"mediumspringgreen":"#00fa9a",
		"mediumturquoise":"#48d1cc",
		"mediumvioletred":"#c71585",
		"midnightblue":"#191970",
		"mintcream":"#f5fffa",
		"mistyrose":"#ffe4e1",
		"moccasin":"#ffe4b5",
		"navajowhite":"#ffdead",
		"navy":"#000080",
		"oldlace":"#fdf5e6",
		"olive":"#808000",
		"olivedrab":"#6b8e23",
		"orange":"#ffa500",
		"orangered":"#ff4500",
		"orchid":"#da70d6",
		"palegoldenrod":"#eee8aa",
		"palegreen":"#98fb98",
		"paleturquoise":"#afeeee",
		"palevioletred":"#d87093",
		"papayawhip":"#ffefd5",
		"peachpuff":"#ffdab9",
		"peru":"#cd853f",
		"pink":"#ffc0cb",
		"plum":"#dda0dd",
		"powderblue":"#b0e0e6",
		"purple":"#800080",
		"red":"#ff0000",
		"rosybrown":"#bc8f8f",
		"royalblue":"#4169e1",
		"saddlebrown":"#8b4513",
		"salmon":"#fa8072",
		"sandybrown":"#f4a460",
		"seagreen":"#2e8b57",
		"seashell":"#fff5ee",
		"sienna":"#a0522d",
		"silver":"#c0c0c0",
		"skyblue":"#87ceeb",
		"slateblue":"#6a5acd",
		"slategray":"#708090",
		"snow":"#fffafa",
		"springgreen":"#00ff7f",
		"steelblue":"#4682b4",
		"tan":"#d2b48c",
		"teal":"#008080",
		"thistle":"#d8bfd8",
		"tomato":"#ff6347",
		"turquoise":"#40e0d0",
		"violet":"#ee82ee",
		"violetred":"#d02090",
		"wheat":"#f5deb3",
		"white":"#ffffff",
		"whitesmoke":"#f5f5f5",
		"yellow":"#ffff00",
		"yellowgreen":"#9acd32"
	}[name.toLowerCase()] || null;
}

// Выводит массив [R,G,B], где R,G,B - составляющие цветовой схемы RGB для указанного цвета
// @param { color - название цвета }
// @return { string[3] }
function ColorToRGB(color)
{
	color = ColorToHex(color);
 
    let matches = color.match(/^#?([\dabcdef]{2})([\dabcdef]{2})([\dabcdef]{2})$/i);

    if (!matches) return false;
  
    for (var i = 1, rgb = new Array(3);  i <= 3; i++) 
  	    rgb[i-1] = parseInt(matches[i], 16);
  
    return rgb;
}
// Возвращает имя домена
// @param { url - адрес страницы }
// @return { string }
function getDomainName (url) {
	let domainName = url.split(/\/+/g)[1];
	return domainName;
}
// Возвращает язык пользователя
// @return { string }
function getUserLang () {
	let lang = (navigator.language || navigator.systemLanguage || navigator.userLanguage || 'ru').substr(0,2).toLowerCase();
	return lang;
}
//---------------------------------------
// Usage
// console.log(ColorToHex('black'));
/*
let rgbArr = SplitRGB("white");
console.log(rgbArr[0]);
console.log(rgbArr[1]);
console.log(rgbArr[2]);
*/
//console.log(getDomainName("http://kbyte.ru/ru/Forums")); // kbyte.ru
//console.log(getUserLang()); // 'ru'
/*
execscr(`function add(x,y){
     return x + y;
}`);

cl(add(5,4));
*//*
let users = ["Tom", "Bob", "Sam"];
forof(users);
forof('Hello');
__iter__(1,9);
*//*
let node = byId('example');
node.style.color = 'lime';
*//*
console.log(var_dump(1)); // вывод результата в консоль
var_dump(1, 'alert'); // вывод результата в окне с помощью функции alert()
var_dump(1, 'body'); // вывод результата на странице
*/
// Example (for var_dump_rec()):
/*
let employees = [
    { id: '1', sex: 'm', city: 'Paris' }, 
    { id: '2', sex: 'f', city: 'London' },
    { id: '3', sex: 'f', city: 'New York' },
    { id: '4', sex: 'm', city: 'Moscow' },
    { id: '5', sex: 'm', city: 'Berlin' }
]
console.log(var_dump_rec(employees));
*/
// Тесты для функций поиска дочерних узлов
// let node = byId('test');
// console.log(firstChild(node).innerHTML);
// console.log(lastChild(node).innerHTML);
// console.log(htmlEntities('<p>&"IS"</p>')); // &lt;p&gt;&amp;&quot;IS&quot;&lt;/p&gt;
