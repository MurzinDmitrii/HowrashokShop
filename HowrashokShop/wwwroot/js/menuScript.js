window.onload = function () {
    var currentLocation = window.location.pathname;
    if (currentLocation == "/") {
        document.getElementById('menu').children[0].children[0].classList.add("current");
    }
    if (currentLocation == "/Arhive") {
        document.getElementById('menu').children[3].children[0].classList.add("current");
    }
}