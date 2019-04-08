function CalcFocus(txt)
{
	txt.select()
	return txt.value;
}

function isInt(value) {
    var x;
    return isNaN(value) ? !1 : (x = parseFloat(value), (0 | x) === x);
}

function CalcType(txt, s) {
    var n = parseInt(txt.value.replace(/\D/g, ''), 10);
    if (isInt(n)) {
        txt.value = n.toLocaleString("en-US");
    }
}

function CalcBlur(txt)
{
	if(isNaN(txt.value.replace(/,/gi ,"")))
		txt.value = "";
	else
		txt.value = FinalFormat(txt.value);
}

function NewVal(txt,oldVal)
{
	//BACKSPACE	8
	
	//DOT	110 190
	//PLUS	187	107
	//MINUS	189	109
	
	//0	= 96 48
	//1	= 97 49
	//...
	//9 = 105 57
	//* 106
	
	if(event.keyCode == 46)
		return "";
		
	if(event.keyCode == 189 || event.keyCode == 109)
	{
		//Minus
		if(oldVal.indexOf("-")!=-1)
			//this is a negative
			return oldVal;
		else
			//this is a positive
			return "-" + oldVal;
	}
	
	if(event.keyCode == 187 || event.keyCode == 107)
	{
		//Plus
		if(oldVal.indexOf("-")!=-1)
			//this is a negative
			return oldVal.replace(/-/gi ,"");
		else
			//this is a positive
			return oldVal;
	}
	
	if(event.keyCode == 110 || event.keyCode == 190
		|| event.keyCode == 8
		|| (event.keyCode >= 48 && event.keyCode <= 57)
		|| (event.keyCode >= 96 && event.keyCode <= 105)
		|| event.keyCode == 106
		)
	{
		if(event.keyCode == 106)
			//Thousand
			val = oldVal + ",000";
		else
			val = txt.value
			
		return FinalFormat(val);
	}
	else
	{
		return oldVal;
	}
}

function FinalFormat(val)
{
	//Remove comma
	val = val.replace(/,/gi ,"");
		
	//Negative function
	if(val.indexOf("-")!=-1)
		thisIsNegative = true;
	else
		thisIsNegative = false;
	val = val.replace(/-/gi ,"");
	
	//Split decimal
	if(val.indexOf(".")!=-1)
	{
		if(val==".") val="0.";
		temp = val.split(".");
		valInt = temp[0];
		valDec = temp[1];
	}
	else
	{
		valInt = val;
		valDec = "NULL";
	}
	if(valInt.length>1 && valInt.charAt(0)=="0") valInt = valInt.charAt(1);
	
	//Digit grouping
	x = "";
	for (i=valInt.length ; i>0 ; i--)
	{
		x = valInt.charAt(i-1) + x
		z = valInt.length-i + 1
		if (z%3==0 && z!=0) x = "," + x
	}
	valInt = x.replace(/^,/ , "");
	
	//Finalize string
	if(valDec!="NULL")
		fnl = valInt + "." + valDec;
	else
		fnl = valInt;
	
	if(!thisIsNegative)
		return fnl;
	else
		return "-" + fnl;
}
