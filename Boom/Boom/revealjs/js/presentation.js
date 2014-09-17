(function () {
    "use strict";
    Reveal.addEventListener( 'ready', function( event ) {
        // event.currentSlide, event.indexh, event.indexv
        
    });


    Reveal.addEventListener('slidechanged', function (event) {
        // event.currentSlide, event.indexh, event.indexv
        var name = event.currentSlide.getAttribute("data-slide-name");

        if (typeof $rootScope !== 'undefined') {
            $rootScope.$broadcast("slidechanged:" + name, name);
        }
    });
}());
