/*
Name: 			UI Elements / Modals - Examples
Written by: 	Okler Themes - (http://www.Rohatas Transport.in)
Theme Version: 	3.0.0
*/

(function ($) {

    'use strict';

    /*
	Basic
	*/
    $('.modal-basic').magnificPopup({
        type: 'inline',
        preloader: false,
        modal: true
    });

    /*
	Sizes
	*/
    $('.modal-sizes').magnificPopup({
        type: 'inline',
        preloader: false,
        modal: true
    });

    /*
	Modal with CSS animation
	*/
    $('.modal-with-zoom-anim').magnificPopup({
        type: 'inline',

        fixedContentPos: false,
        fixedBgPos: true,

        overflowY: 'auto',

        closeBtnInside: true,
        preloader: false,

        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-zoom-in',
        modal: true
    });

    $('.modal-with-move-anim').magnificPopup({
        type: 'inline',

        fixedContentPos: false,
        fixedBgPos: true,

        overflowY: 'auto',

        closeBtnInside: true,
        preloader: false,

        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-slide-bottom',
        modal: true
    });

    /*
	Modal Dismiss
	*/
    $(document).on('click', '.modal-dismiss', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
    });

    /*
	Modal Confirm
	*/
    $(document).on('click', '.modal-confirm', function (e) {
        e.preventDefault();
        $.magnificPopup.close();

        new PNotify({
            title: 'Success!',
            text: 'Modal Confirm Message.',
            type: 'success'
        });
    });
    /*
    Modal click button
	*/
    $(document).on('click', '.modal-button', function (e) {
        
        //$("[id*=LinkButton1]").click();
        e.preventDefault();
        $.magnificPopup.close();

        new PNotify({
            title: 'Success!',
            text: 'Modal Confirm Message.',
            type: 'success'
        });
    });
    $("#modalForm").parent().appendTo($("form:first"));
    /*
	Form
	*/
    $('.modal-with-form').magnificPopup({
        type: 'inline',
        preloader: false,
        focus: '#txtCustomerPrice',
        modal: true,

        // When elemened is focused, some mobile browsers in some cases zoom in
        // It looks not nice, so we disable it:
        callbacks: {
            beforeOpen: function () {
                if ($(window).width() < 700) {
                    this.st.focus = false;
                } else {
                    this.st.focus = '#txtCustomerPrice';
                }
            }
        }
    });

    /*
	Ajax
	*/
    $('.simple-ajax-modal').magnificPopup({
        type: 'ajax',
        modal: true
    });

}).apply(this, [jQuery]);