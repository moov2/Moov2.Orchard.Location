(function () {

    var maps = {};

    function selectFirst(selector, element) {
        if (typeof (element) === 'undefined') {
            element = document;
        }
        var result = element.querySelectorAll(selector);
        if (result.length == 0) {
            return undefined;
        }
        return result[0];
    }

    function setUpMap(contentItemId) {
        var map = {};
        map.container = selectFirst('.js-location-map--container[data-content-item-id="' + contentItemId + '"]');
        if (typeof (map.container) === 'undefined') {
            return;
        }
        map.element = selectFirst('.js-location-map--map', map.container);
        if (typeof (map.element) === 'undefined') {
            return;
        }
        var item = selectFirst('.js-location-map--latitude', map.container)
        if(typeof(item) === 'undefined') {
            return;
        }
        map.latitude = parseFloat(item.value, 10);
        item = selectFirst('.js-location-map--longitude', map.container)
        if (typeof (item) === 'undefined') {
            return;
        }
        map.longitude = parseFloat(item.value, 10);
        var location = { lat: map.latitude, lng: map.longitude };
        map.map = new google.maps.Map(map.element, {
            center: location,
            zoom: 17
        });
        map.marker = new google.maps.Marker({
            position: location,
            map: map.map
        });
        maps[contentItemId] = map;
    }

    function mapCallback(contentItemId) {
        if (typeof (maps[contentItemId]) === 'undefined') {
            setUpMap(contentItemId);
        }
    }

    function init() {
        window.mapCallback = mapCallback;
        if (typeof (window.loadedMaps) !== 'undefined') {
            for (var i = 0; i < window.loadedMaps.length; i++) {
                setUpMap(loadedMaps[i]);
            }
            window.loadedMaps = [];
        }
    }

    function ready(fn) {
        if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
            fn();
        } else {
            document.addEventListener('DOMContentLoaded', fn);
        }
    }

    ready(init);
})();