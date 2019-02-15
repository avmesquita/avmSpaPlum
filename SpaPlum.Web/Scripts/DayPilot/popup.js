/* Copyright © 2005 - 2009 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/
*/

var popup = {};

popup.id = "__kfaowefna_"; // obscure id

popup.show = function(html) {

	if (!this.div) {
		this.create();
	}
	else {
		this.div.style.display = '';
	}

	var delayed = function(p, innerHTML) {
    	return function() {
        	p.setInnerHTML(p.id + "iframe", innerHTML);
        }
    };
        
	window.setTimeout(delayed(this, html), 0);

}

popup.create = function() {
	var hide = document.createElement("div");
	hide.id = this.id + "hide";
	hide.style.position = 'absolute';
	hide.style.left = "0px";
	hide.style.top = "0px";
	hide.style.width = "100%";
	hide.style.height = "100%";
	hide.style.filter = "alpha(opacity=55)";
	hide.style.MozOpacity = "0.55";
	hide.style.backgroundColor = "gray";

	document.body.style.height = '100%';

	document.body.appendChild(hide);

	var iframe = document.createElement("iframe");
	iframe.id = this.id + "iframe";
	iframe.name = this.id + "iframe";
	iframe.style.borderWidth = '0px';
	iframe.style.width = '100%';
	iframe.style.height = '380px';
	//iframe.src = 'http://www.google.com';

	var div = document.createElement("div");
	div.id = this.id + 'popup';
	div.style.border = '1px solid red';
	div.style.position = 'absolute';
	div.style.left = '50%';
	div.style.marginLeft = '-45%';
	div.style.top = '20px';
	div.style.width = '90%';
	div.style.height = '460px';
	div.style.backgroundColor = 'white';

	var ok = document.createElement("div");
	ok.innerHTML = "<div style='background-color: red; color:white; font-family: Tahoma, Arial, Sans-serif; font-size: 20pt'>AJAX Error <a href='javascript:popup.hide()' style='color:black'>[Close]</a></div>";

	div.appendChild(ok);
	div.appendChild(iframe);

	document.body.appendChild(div);

	this.div = div;
	this.hideDiv = hide;
};


popup.setInnerHTML = function(id, innerHTML) {
	var frame = window.frames[id];

	var doc = frame.contentWindow || frame.document || frame.contentDocument;
	//alert(id + ' ' + frame);
	if (doc.document) {
		doc = doc.document;
	}

	doc.body.innerHTML = innerHTML;
};

popup.hide = function() {
	if (this.div) {
		this.div.style.display = 'none';
		this.hideDiv.style.display = 'none';
	}
}
