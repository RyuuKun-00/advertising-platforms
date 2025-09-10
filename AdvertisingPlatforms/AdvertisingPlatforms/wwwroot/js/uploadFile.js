

(async function ($) {

  if(!$) {
    return;
  }
 "use strict";

  const uploadUrl = "/api/upload";

  const appParams =  await getParameters();

 ["dragover", "drop"].forEach(function(event) {
  document.addEventListener(event, function(evt) {
      evt.preventDefault()
      return false
    })
  });

  let setStatus = (text,isError=false) => 
  {
    var hint = $("#uploadForm_Hint");
    hint.html(text);
    if(isError){
      hint.addClass("error");
      hint.removeClass("success");
    }else{
      hint.addClass("success");
      hint.removeClass("error");
    }
  }

 $(".upload-zone-dragover").on("dragenter", function(){
    $(this).addClass('_active');
  });

  $(".upload-zone-dragover").on("dragleave", function(){
    $(this).removeClass('_active');
  });

  $(".upload-zone-dragover").on("drop", function(){
    $(this).removeClass('_active');
      const file = event.dataTransfer?.files[0];
      var a = $(".form-upload-input").get(0);
      processingUploadFile(file)
  });

  $(".form-upload-input").on("change", function(){
    console.log("+++");
    const file = $(this).get(0).files?.[0];
    processingUploadFile(file);
  });

  function isValidFile(file){
    if(!file){
      return "Файл не обнаружен."
    }

    const fileNameExten = file.name.split('.').reverse()[0];
    const allowedExtensions = appParams.allowedExtensions;

    if (!allowedExtensions.includes("."+fileNameExten)) {
      return `Недопустимый тип файла. Разрешены только:<br>${appParams.allowedExtensions.join(",")}.`
    }

    const allowedTypes = appParams.allowedMimeTypes;
    if (!allowedTypes.includes(file.type)) {
      return `Недопустимый контент файла. Разрешены только:<br>${appParams.allowedMimeTypes.join(",")}.`
    }

    const maxSizeInBytes = appParams.maxSize;
    if (file.size > maxSizeInBytes) {
        return `Недопустимый размер файла. Максимальный разрешенный размер:<br>\n${maxSizeInBytes} байт.`
    }

    return null;
  }

  function processingUploadFile(file) {
    
    var error = isValidFile(file);

    if(error !== null){
      setStatus(error,true);
      return false;
    }
         
    const dropZoneData = new FormData()
    const xhr = new XMLHttpRequest()

    dropZoneData.append("uploadedFile", file)

    xhr.open("POST", uploadUrl, true)

    xhr.send(dropZoneData)

    xhr.onload = function () {
      if (xhr.status == 200) {
        setStatus("Файл успешно загружен.");
      }else{
        setStatus("Ошибка загрузки файла. Подробности в консоли.",true);
      }
      console.log("Загрузка "+file.name + " результат:\n"+ xhr.responseText);

      $("#uploadForm").get(0).reset();
    }
  }


})(jQuery);