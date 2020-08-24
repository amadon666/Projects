/**
  js_lang - поддержка языка JavaScript
  asm_lang - поддержка языка Assembler
  cpp_lang - поддержка языка C++
  pascal_lang - поддержка языка PascalABC.NET
  php_lang - поддержка языка PHP
  ts_lang - поддержка языка TypeScript
**/
let preArray = document.querySelectorAll('pre');

(function() {

let numbers = /\b[0-9]+\b/g;
// Названия функций
let nameFunc = /([a-z]+)\((.+)?\)/gim;

	preArray.forEach( function(item, i) {
		let pre = item; // Небработанный блок <pre>
		let preHighlighter = ""; // Обработанный блок <pre>

	if (item.className == 'js_lang') {	
		// Ключевые слова JS
		let keywordsJsRegex = /\b(var|let|if|else|switch|case|default|function|return|export|import|global|const|typeof|for|while|do|break|continue|delete|prototype|class|constructor)\b/g;
		// Другие ключевые слова JS
		let anyJsRegex = /\b(this|null|undefined|true|false)\b/g;
		// Самые известные функции JS
		let globalFuncJs = /\b(console|document|log|Math|Regex|Array|Symbol|Number|Object|Reflect|Proxy|Boolean|String|alert|prompt|confirm)\b/g;
		// Комментарии
        let comments = /\/\/(.+?)/gi;
        let operators = /\b(=|==)\b/gim;
	
		preHighlighter = pre.innerHTML  
		    .replace(operators, '<span class = "oper">$&</span>')
		    .replace(/(\")(.+?)(\")/gi, '<span class = "string">$&</span>')
			.replace(keywordsJsRegex, '<span class = "keywords">$&</span>')
			.replace(anyJsRegex, '<span class = "anywords">$&</span>')
			.replace(globalFuncJs, '<span class = "globalFunctions">$&</span>')
			.replace(numbers, '<span class = "numbers">$&</span>')	
			.replace(/((\(|\)|\{|\}))+/gim, '<span class = "punc">$&</span>')
			.replace(comments, '<span class = "com">$&</span>');
	    
	} else if (item.className == 'asm_lang') {
		let specialMacroses = /\b(bss|data|text|CODE|DATA|STACK|CONST|BREAK|CONTINUE|WHILE|ENDW|IF|ENDIF)\b/gm;
		let keywordsAsm = /\b(add|sub|mov|push|pop|pusha|popa|pushf|popf|mul|div|imul|idiv|inc|dec|neg|and|or|xor|not|int|iret|cli|sti|xchg|rep|repz|in|out|sal|sar|rcl|rcr|times|cmp|shl|shr|call|ret|equ)\b/gim;
		let anyAsm = /\b(section|je|jne|ja|jb|jna|jg|jl|jz|jc|js|jo|jnz|jp)\b/gim;
		let registersAsm = /\b(ax|bx|cx|dx|eax|ebx|ecx|edx)\b/gim;
		let comments = /\;(.*)?/gim;

		preHighlighter = pre.innerHTML
		    .replace(specialMacroses, '<span class = "macrosesAsm">$&</span>')
			.replace(keywordsAsm, '<span class = "keywords">$&</span>')
			.replace(registersAsm, '<span class = "registers">$&</span>')
			.replace(comments, '<span class = "com">$&</span>')
			.replace(anyAsm, '<span class = "anyAsm">$&</span>');

	} else if (item.className == 'pascal_lang') {
		let keywordsPascal = /\b(type|class|record|begin|end|var|new|constructor|procedure|function|uses|program|while|for|repeat|until|do|to|loop|label|foreach|const|array|of|with|match|set|in|is|as|if|then|else|file|sequence|yield|case|default|try|except|finally|unit|interface|initialization|implementation|finalization|on|div|mod|shr|lock|library|external|internal|destructor|property|sealed|inherited|virtual|override|reintroduce|abstract|operator|and|or|xor|not|new|implicit|explicit|static|extensionmethod|auto|forward|params|where|when|downto|typeof|sizeof|overload|event|shl|template)\b/gim;
		let anywordsPascal = /\b(public|private|protected|internal)\b/gim;
		let typesPascal = /\b(integer|boolean|double|string|char|shortint|smallint|longint|int64|byte|word|longword|cardinal|uint64|bigInteger|real|single|decimal)\b/gim;
		let numbersPascal = /\b\d+\b/gim; 
		let specialWordsPascal = /\b(goto|self|break|continue|exit|raise|true|false)\b/gim;
		// Директивы PascalABC.NET
		let directivesPascal = /\{(.*?)\}/gim;
		// Комментарии
        let comments = /\/\/(.+)/gi;
        /// Документирующие комментарии
        let docComments = /\/\/\/(.+)/gi;

		preHighlighter = pre.innerHTML
		    .replace(keywordsPascal, '<span class = "keywords">$&</span>')
			.replace(typesPascal, '<span class = "keywords">$&</span>')
			.replace(numbersPascal, '<span class = "numbers">$&</span>')
			.replace(comments, '<span class = "com">$&</span>')
			.replace(docComments, '<span class = "com">$&</span>')
			.replace(anywordsPascal, '<span class = "anyAsm">$&</span>')
			.replace(specialWordsPascal, '<span class = "anywords">$&</span>')
			.replace(directivesPascal, '<span class = "directivesPascal">$&</span>');

	} else if (item.className == 'cpp_lang') { // FIXME: Исправить баги
		let typeDataCpp = /\b(int|bool|char|char16_t|char32_t|char8_t|double|float|long|short|signed|unsigned|void|wchar_t)\b/;
		let keywordsCpp = /\b(asm|if|auto|break|case|catch|const|constexpr|constinit|default|delete|do|else|enum|consteval|continue|decltype|dynamic_cast|class|explicit|export|extern|false|final|for|friend|goto|inline|mutable|namespace|new|noexcept|nullptr|override|private|public|protected|register|reinterpret_cast|requires|return|sizeof|static|static_assert|static_cast|struct|switch|template|this|thread_local|throw|true|try|typedef|typeid|typename|union|using|virtual|volatile|while)\b/gm;
		// Слова - операторы С++
		let wordOpers = /\b(alignas|alignof|and|std|and_eq|bitand|bitor|co_await|co_return|co_yield|compl|concept|const_eval|not|not_eq|or|or_eq|xor|xor_eq)\b/gm;
		// Глобальные самые известные функции C++
		let globalFuncCpp = /\b(cout|cin|clog|cerr)\b/gm;

		preHighlighter = pre.innerHTML	
			.replace(wordOpers, '<span class = "globalOpersCpp">$&</span>')
			.replace(keywordsCpp, '<span class = "keywords">$&</span>')
			.replace(globalFuncCpp, '<span class = "globalFunctions">$&</span>')
			.replace(typeDataCpp, '<span class = "typesCpp">$&</span>');
	} else if (item.className == 'php_lang') {

		let keywordsPhp = /\b(function|static|class|return|final|global|try|xor|abstract|callable|const|do|enddeclare|endwhile|finally|goto|instanceof|namespace|yield|and|case|endfor|for|if|insteadof|use|catch|declare|else|endforeach|foreach|interface|or|require|throw|var|as|default|elseif|endif|include|while|trait)\b/gm;
		let anyPhp = /\b(public|new|extends|implements|break|continue|clone|empty|endswitch|include_once|list|private|protected|switch)\b/gm;
		let wordOpers = /\b(echo|self|die|__halt_compiler|unset|eval|array|exit|isset|print|require_once)\b/gm;
	
		preHighlighter = pre.innerHTML
			.replace(keywordsPhp, '<span class = "keywords">$&</span>')
			.replace(wordOpers, '<span class = "globalOpersCpp">$&</span>')
			.replace(nameVariablesPhp, '<span class = "varNames">$&</span>')
			.replace(numbers, '<span class = "numbers">$&</span>')
			.replace(nameFunc, '<span class = "function">$&</span>')
			.replace(anyPhp, '<span class = "anyAsm">$&</span>');
	} else if (item.className == 'ts_lang') {
		let keywordsTs = /\b(class|break|case|var|module|finally|for|super|return|extends|implements|try|do|throw|instanceof|enum|while|interface|yield|catch|export|let|function|const)\b/gm;
		let typesDataTs = /\b(number|string|boolean|any|never|void)\b/gm;
		let anyTs = /\b(public|package|null|private|this|true|false|static|new|keyof)\b/gm;

		preHighlighter = pre.innerHTML
			.replace(keywordsTs, '<span class = "keywords">$&</span>')
			.replace(typesDataTs, '<span class = "macrosesAsm">$&</span>')
			.replace(anyTs, '<span class = "keywords">$&</span>')
			.replace(numbers, '<span class = "numbers">$&</span>')
			.replace(nameFunc, '<span class = "function">$&</span>');
	}
	pre.innerHTML = preHighlighter;
});
})();
