(function ($) {

    if(!$) {
        return;
    }

  "use strict";

  $("#btnSearch").on("click",async function(){
    await InitHTMLElements()
  });

    $("#boxSearch").keyup( async function (e) {
        if (e.keyCode === 13) {
         await InitHTMLElements()
        }
    });

    async function InitHTMLElements(){
        var searchString = $("#boxSearch").get(0).value.trim();

        var platforms = await searchAdvertisingPlatforms(searchString);

        if(platforms.error){
            var errors = platforms.error.replaceAll("\n","<br>")
            $(".textErrorSearch").html(errors);
            return;
        }
        $(".textErrorSearch").html("");
        $("#contetnt").removeClass("d-none");

        var htmlPlatforms="";
        platforms.advertisingPlatforms.forEach(element => {
            var loc = element.nameLocation;
            var platforms = element.advertisingPlatforms;

            var advertisingPlatforms="";
            platforms.forEach(element=>{
                advertisingPlatforms+=`<span>${element}</span>`;
            });

            searchString = GetLocation(loc,searchString);

            htmlPlatforms+=`
            <details class="location">
                <summary class="nameLocation">${searchString}</summary>
                <div class="group">
                    ${advertisingPlatforms}
                </div>
            </details>
            `;
            
        });

        $("#AdvertisingPlatforms").html(htmlPlatforms);
    }

    function GetLocation(sub,loc){
        var locations = loc.split("/").filter(Boolean).reverse();
        var result="";
        var trigger=false;
        for (const e of locations) {
            if(e === sub){
                trigger = true;
            }
            if(trigger){
                result="/"+e+result;
            }
        }
        return result;
    }

})(jQuery);
