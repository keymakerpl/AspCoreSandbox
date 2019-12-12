

// W JavaScript jest tak dziwnie, że nie ma namespace, a pakuje się js przez jQuery w funkcję. ready sprawdza czy strona jest gotowa
$(document).ready(function () {

    console.log("Test Message....");

    // Znak dolar $ to alias do jQuery - biblioteka ułatwiająca korzystanie z JavaScript

    var theForm = $("#theForm"); // Znak hash # to alias do elementu po ID, zwraca kolekcję
    theForm.hide();

    var button = $("#buyButton");
    button.on("click", function () { console.log("Buying") });

    var productInfo = $(".product-props li"); // Odnosimy się do childa li
    productInfo.on("click", function () { console.log("Clicked on " + $(this).text()) });

    // Dolar w nazwie zmiennej jest umowny, oznacza, że to zmienna jQuery
    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form")

    $loginToggle.on("click", function () { $popupForm.fadeToggle(200); })

});