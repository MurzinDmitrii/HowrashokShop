window.onload = function () {
    var currentLocation = window.location.pathname;
    if (currentLocation == "/") {
        document.getElementById('menu').children[0].children[0].classList.add("current");
    }
    if (currentLocation == "/Index") {
        document.getElementById('menu').children[0].children[0].classList.add("current");
    }
    if (currentLocation == "/Comments") {
        document.getElementById('menu').children[1].children[0].classList.add("current");
    }
    if (currentLocation == "/Arhive") {
        document.getElementById('menu').children[2].children[0].classList.add("current");
    }
    if (currentLocation == "/Auth") {
        document.getElementById('menu').children[3].children[0].classList.add("current");
    }
    if (currentLocation == "/Profile/Edit") {
        document.getElementById('menu').children[3].children[0].classList.add("current");
    }
    if (currentLocation == "/Profile/Create") {
        document.getElementById('menu').children[3].children[0].classList.add("current");
    }
}