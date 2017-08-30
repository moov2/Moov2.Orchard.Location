(function () {

    var $locationFields = $('.js-location-field'),
        $coordFields = $('.js-location-field-coords'),
        $showAllLink = $('.js-show-location-all'),
        $showCoordsLink = $('.js-show-location-coordinates'),
        HIDDEN_CLASS = 'location-admin--hidden';

    var showLocations = function () {
        $locationFields.removeClass(HIDDEN_CLASS);
    };

    var showCoords = function () {
        $coordFields.removeClass(HIDDEN_CLASS);
    };

    var addListeners = function () {
        $showAllLink.on('click', function (e) {
            e.preventDefault();
            $showAllLink.addClass(HIDDEN_CLASS);
            showLocations();
        });

        $showCoordsLink.on('click', function (e) {
            e.preventDefault();
            $showCoordsLink.addClass(HIDDEN_CLASS);
            showCoords();
        });
    };

    addListeners();

})();