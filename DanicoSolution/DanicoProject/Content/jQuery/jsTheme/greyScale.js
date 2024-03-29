/*
 *  jQuery $.greyScale Plugin v0.2
 *  Written by Andrew Pryde (www.pryde-design.co.uk)
 *  Date: Mon 1 Aug 2011
 *  Licence: MIT Licence
 *
 *  Copyright (c) 2011 Andrew Pryde
 *  Permission is hereby granted, free of charge, to any person obtaining a copy of this 
 *  software and associated documentation files (the "Software"), to deal in the Software
 *  without restriction, including without limitation the rights to use, copy, modify, merge,
 *  publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons 
 *  to whom the Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in all copies or 
 *  substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 *  BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
 *  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 *  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

(function($){

  $.fn.greyScale = function(args) {
    $options = $.extend({
      fadeTime: $.fx.speeds._default,
      reverse: false,
	  myHeight: '',
	  myWidth: ''
    }, args);
    function greyScale(image, width, height) {
      can = $('<canvas>')
        .css({
          'display' : 'none',
          'left' : '0',
          'position' : 'absolute',
          'top' : '0'
        })
        .attr({
          'width': width,
          'height': height
        })
        .addClass('gsCanvas');
      ctx = can[0].getContext('2d');
	  if($options.myHeight) height = $options.myHeight;
	  if($options.myWidth) width = $options.myWidth;
      ctx.drawImage(image, 0, 0, width, height);

      imageData = ctx.getImageData(0, 0,  width, height);
      px = imageData.data;
      for (i = 0; i < px.length; i+= 4) {
        grey = px[i] * .3 + px[i+1] * .59 + px[i+2] * .11;
        px[i] = px[i+1] = px[i+2] = grey;
      }
      ctx.putImageData(imageData, 0, 0);
      return can;
    }
    if ($.browser.msie) {
      // IE doesn't support Canvas so use it's horrible filter syntax instead
      this.each(function(){
        var greyscale = $options.reverse ? 0 : 1;
        $(this).css({
          'filter': 'progid:DXImageTransform.Microsoft.BasicImage(grayscale=' + greyscale + ')',
          'zoom': '1'
        });
        $(this).hover(function() {
          var greyscale = $options.reverse ? 1 : 0;
          $(this).css({
            'filter': 'progid:DXImageTransform.Microsoft.BasicImage(grayscale=' + greyscale + ')'
          });
        }, function() {
          var greyscale = $options.reverse ? 0 : 1;
          $(this).css('filter', 'progid:DXImageTransform.Microsoft.BasicImage(grayscale=' + greyscale + ')');
        });
      });
    } else {
      this.each(function(index) {
        $(this).wrap('<div class="gsWrapper">');
        gsWrapper = $(this).parent();
        gsWrapper.css({
          'position' : 'relative',
          'display' : 'inline-block'
        });
        if (window.location.hostname !== this.src.split('/')[2]) {
          // If the image is on a different domain proxy the request
         $.getImageData({
            url: $(this).attr('src'),
            success: $.proxy(function(image) {
                can = greyScale(image, image.width, image.height);
                if ($options.reverse) { can.appendTo(gsWrapper).css({"display" : "block", "opacity" : "0"}); }
                else { can.appendTo($(this)).fadeIn($options.fadeTime); }
              }, gsWrapper),
            error: function(xhr, text_status){
              // silently fail on error
            }
          });
        } else { // If the image is on the same domain don't proxy the request
          can = greyScale($(this)[0], $(this).width(), $(this).height());
          if ($options.reverse) { can.appendTo(gsWrapper).css({"display" : "block", "opacity" : "0"}); }
          else { can.appendTo(gsWrapper).fadeIn($options.fadeTime); }
        }
    });

    $(this).parent().delegate('.gsCanvas', 'mouseover mouseout', function(event) {
      over = $options.reverse ? 1 : 0;
      out = $options.reverse ? 0 : 1;
      (event.type == 'mouseover') && $(this).stop().animate({'opacity': over}, $options.fadeTime);
      (event.type == 'mouseout') && $(this).stop().animate({'opacity': out}, $options.fadeTime); 
    });
  }
  };
})( jQuery );

/*
 *
 *  jQuery $.getImageData Plugin 0.3
 *  http://www.maxnov.com/getimagedata
 *  
 *  Written by Max Novakovic (http://www.maxnov.com/)
 *  Date: Thu Jan 13 2011
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  
 *  Includes jQuery JSONP Core Plugin 2.1.4
 *  http://code.google.com/p/jquery-jsonp/
 *  Copyright 2010, Julian Aubourg
 *  Released under the MIT License.
 * 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  
 *  Copyright 2011, Max Novakovic
 *  Dual licensed under the MIT or GPL Version 2 licenses.
 *  http://www.maxnov.com/getimagedata/#license
 * 
 */
(function(c,g){function n(){}function o(a){s=[a]}function e(a,j,k){return a&&a.apply(j.context||j,k)}function h(a){function j(b){!l++&&g(function(){p();q&&(t[d]={s:[b]});z&&(b=z.apply(a,[b]));e(a.success,a,[b,A]);e(B,a,[a,A])},0)}function k(b){!l++&&g(function(){p();q&&b!=C&&(t[d]=b);e(a.error,a,[a,b]);e(B,a,[a,b])},0)}a=c.extend({},D,a);var B=a.complete,z=a.dataFilter,E=a.callbackParameter,F=a.callback,R=a.cache,q=a.pageCache,G=a.charset,d=a.url,f=a.data,H=a.timeout,r,l=0,p=n;a.abort=function(){!l++&&
p()};if(e(a.beforeSend,a,[a])===false||l)return a;d=d||u;f=f?typeof f=="string"?f:c.param(f,a.traditional):u;d+=f?(/\?/.test(d)?"&":"?")+f:u;E&&(d+=(/\?/.test(d)?"&":"?")+encodeURIComponent(E)+"=?");!R&&!q&&(d+=(/\?/.test(d)?"&":"?")+"_"+(new Date).getTime()+"=");d=d.replace(/=\?(&|$)/,"="+F+"$1");q&&(r=t[d])?r.s?j(r.s[0]):k(r):g(function(b,m,v){if(!l){v=H>0&&g(function(){k(C)},H);p=function(){v&&clearTimeout(v);b[I]=b[w]=b[J]=b[x]=null;i[K](b);m&&i[K](m)};window[F]=o;b=c(L)[0];b.id=M+S++;if(G)b[T]=
G;var O=function(y){(b[w]||n)();y=s;s=undefined;y?j(y[0]):k(N)};if(P.msie){b.event=w;b.htmlFor=b.id;b[I]=function(){/loaded|complete/.test(b.readyState)&&O()}}else{b[x]=b[J]=O;P.opera?(m=c(L)[0]).text="jQuery('#"+b.id+"')[0]."+x+"()":b[Q]=Q}b.src=d;i.insertBefore(b,i.firstChild);m&&i.insertBefore(m,i.firstChild)}},0);return a}var Q="async",T="charset",u="",N="error",M="_jqjsp",w="onclick",x="on"+N,J="onload",I="onreadystatechange",K="removeChild",L="<script/>",A="success",C="timeout",P=c.browser,
i=c("head")[0]||document.documentElement,t={},S=0,s,D={callback:M,url:location.href};h.setup=function(a){c.extend(D,a)};c.jsonp=h})(jQuery,setTimeout);
(function(c){c.getImageData=function(a){var f=/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;if(a.url){var g=location.protocol==="https:",e="";e=a.server&&f.test(a.server)&&a.server.indexOf("https:")&&(g||a.url.indexOf("https:"))?a.server:"//img-to-json.appspot.com/?callback=?";c.jsonp({url:e,data:{url:escape(a.url)},dataType:"jsonp",timeout:1E4,success:function(b){var d=new Image;c(d).load(function(){this.width=b.width;this.height=b.height;typeof a.success==typeof Function&& a.success(this)}).attr("src",b.data)},error:function(b,d){typeof a.error==typeof Function&&a.error(b,d)}})}else typeof a.error==typeof Function&&a.error(null,"no_url")}})(jQuery);
