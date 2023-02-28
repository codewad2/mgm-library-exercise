// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function debounce(func, timeout) {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => {
            func.apply(this, args);
        }, timeout);
    };
}

let searchForm = $('#searchForm');
let searchInput = searchForm.find('input[name=searchTerms]');
let bookResults = $('#book-results');

let search = debounce(function () {
    searchForm.submit();
}, 350);

searchInput
    .keyup(search)
    .change(search);

searchForm.submit(function (event) {
    $.ajax({
        type: searchForm.attr('method'),
        url: searchForm.attr('action'),
        data: searchForm.serialize(),
        success: function(data) {
            console.log("success: " + data);
            bookResults.find('.book-row').remove();
            bookResults.append(data);
        }
    });

    event.preventDefault();
});
