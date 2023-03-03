// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function debounce(func, timeout) {
    let timer;
    return function () {
        clearTimeout(timer);
        timer = setTimeout(() => {
            func.apply(this, arguments);
        }, timeout);
    };
}

function ajaxSubmit(form, success) {
    return $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: success
    });
}

let search = function (form) {
    return debounce(function () {
        form.submit();
    }, 350);
};

let memberDetails = $('#member-details');
let checkoutForm = $('#checkout-form');
let checkoutButton = $('.checkout-button');
let checkinForm = $('#checkin-form');
let checkinButton = $('.checkin-button');
let bookSearchForm = $('#book-search-form');
let memberSearchForm = $('#member-search-form');
let bookResults = $('#book-results');
let memberResults = $('#member-results');

function callCheckout(memberId, bookId) {
    checkoutForm.find('input[name=memberId]').val(memberId);
    checkoutForm.find('input[name=bookId]').val(bookId);
    checkoutForm.submit();
}

function callCheckin(bookId) {
    checkinForm.find('input[name=bookId]').val(bookId);
    checkinForm.submit();
}

let bookSearch = search(bookSearchForm);

bookSearchForm.find('input[name=searchTerms]')
    .keyup(function () { bookSearch(); })
    .change(function () { bookSearch(); });

bookSearchForm.submit(function (event) {
    ajaxSubmit(bookSearchForm, function (data) {
        bookResults.find('.book-row').remove();
        bookResults.append(data);
    });

    event.preventDefault();
});

let membersSearch = search(memberSearchForm);

memberSearchForm.find('input[name=searchTerms]')
    .keyup(membersSearch)
    .change(membersSearch);

memberSearchForm.submit(function (event) {
    ajaxSubmit(memberSearchForm, function (data) {
        memberResults.find('.member-row').remove();
        memberResults.append(data);
    });

    event.preventDefault();
});

checkinButton.click(function (event) {
    let button = $(this);
    let bookId = button.data('book-id');

    button.attr('disabled', 'disabled');
    callCheckin(bookId);
});

checkoutButton.click(function (event) {
    let button = $(this);
    let bookId = button.data('book-id');
    let memberId = memberDetails.data('member-id');

    button.attr('disabled', 'disabled');
    callCheckout(memberId, bookId);
});