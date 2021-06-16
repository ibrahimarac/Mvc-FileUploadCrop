$().ready(function () {
   
    var previewAsHtml = $("#preview").html();

    $("#File").change(function () {
        var file = this.files;
        if (file.length != 0) {
            var selectedFile = file[0];
            //console.log(selectedFile);  
            ReadImageFileForPreview(selectedFile);
        }
    })

    var ReadImageFileForPreview = function (file) {
        var reader = new window.FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            
            $("#preview").html("<img src='" + reader.result + "' id='imgPreview' class='img-fluid thumbnail img-responsive' />" + previewAsHtml);
            //$("#imgPreview").attr("src", reader.result);
            var description = "<b>" + file.name + "</b>&nbsp;&nbsp;&nbsp; Boyut : " + ~~(file.size / 1024) + " KB";
            $(".description").html(description);
            $("#submit").show();
            $("#preview").show(200, function () {
                CropAreaActivate();
            });

        }
    }

    var CropAreaActivate = function () {
        $("#imgPreview").Jcrop({
            onSelect: function (crop) {
                console.log(crop);
                $("#Width").val(crop.w);
                $("#Height").val(crop.h);
                $("#X").val(crop.x);
                $("#Y").val(crop.y);
                $("#CropAreaWidth").val($("#imgPreview").width());
                $("#CropAreaHeight").val($("#imgPreview").height());
            }
        });
    }

    $("#preview").on("click","#deletePreview",function () {
        $("#imgPreview").attr('src', '');
        $("#preview").hide(200);
        $("#File").val('');
        $("#submit").hide();
    })

})