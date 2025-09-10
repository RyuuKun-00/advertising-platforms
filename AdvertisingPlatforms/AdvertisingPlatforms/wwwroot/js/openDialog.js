(function ($) {

    if(!$) {
    return;
  }
  "use strict";

  // OPEN DIALOG
  $("#myDialog").on("click",function ({ currentTarget, target }) {
      const dialog = currentTarget;
      const isClickedOnBackDrop = target === dialog;
      if (isClickedOnBackDrop) {
        close();
      }
  });

  $("#myDialog").on("cancel",function () {
    returnScroll();
  });

  $(".upload-file").on("click",function () {
    $("#myDialog").get(0).showModal();
    document.body.classList.add('scroll-lock');
  });

  $(".closeDialogBtn").on("click",function (event) {
      event.stopPropagation();
      close();
  });

  function returnScroll() {
    document.body.classList.remove('scroll-lock')
  }

  function close() {
    $("#myDialog").get(0).close()
    $("#uploadForm_Hint").html("");
    returnScroll()
  }

})(jQuery);
