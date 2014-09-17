(function () {
    "use strict";
    var app = angular.module('boom');

    var getIndexesFromSlide = function(slide, name, indexes) {
        var subslides,
            index;

        if (slide.getAttribute("data-slide-name") == name) {
            return indexes;
        }

        //not on level one, search in subslides
        subslides = slide.querySelectorAll('section');
        for (index = 0; index < subslides.length; index++) {
            indexes.indexv++;

            indexes = getIndexesFromSlide(subslides[index], name, indexes);
            if(typeof indexes !== 'undefined') {
                return indexes;
            }
        }

        return undefined;
     }

    app.factory("revealService", function () {        
        return {
            navigateToSlide: function (name) {
                var slide,
                    slides,
                    index,
                    indexes = undefined;

                slide = angular.element(document.querySelectorAll('section[data-slide-name=' + name + ']'));
                if (slide.length <= 0) {
                    throw "Slide '" + name + "' not found!";
                }

                slides = angular.element(document.querySelectorAll('.slides section'));
                
                for (index = 0; index < slides.length; index++) {
                    indexes = getIndexesFromSlide(slides[index], name, { indexh: index, indexv: 1 });

                    if (typeof indexes !== 'undefined') {
                        Reveal.slide(indexes.indexh, indexes.indexv, 1);
                        return;
                    }
                }

            }
        };
    });
})();