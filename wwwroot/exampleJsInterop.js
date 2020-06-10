window.exampleJsFunctions = {
    showPrompt: function (text) {
        return prompt(text, 'Type your name here');
    },
    displayWelcome: function (welcomeMessage) {
        document.getElementById('welcome').innerText = welcomeMessage;
    },
    returnArrayAsyncJs: function () {
        DotNet.invokeMethodAsync('BlazorSample', 'ReturnArrayAsync')
            .then(data => {
                data.push(4);
                console.log(data);
            });
    },
    sayHello: function (dotnetHelper) {
        return dotnetHelper.invokeMethodAsync('SayHello')
            .then(r => console.log(r));
    },
    showMessage: function (msg) {
        if (confirm(msg) === true) {
            return true;  //你也可以在这里做其他的操作
        } else {
            return false;
        }
    },
    getImgUrl: function (id) {
        /**
        * 文件预览
        */
        var url = null;

        var preview = document.querySelector('img');
        return decodeURI(preview.src);
        //var fileObj = document.getElementById(id).files[0];
        //if (window.createObjcectURL !== undefined) {
        //    url = window.createOjcectURL(fileObj);
        //} else if (window.URL !== undefined) {
        //    url = window.URL.createObjectURL(fileObj);
        //} else if (window.webkitURL !== undefined) {
        //    url = window.webkitURL.createObjectURL(fileObj);
        //}
        //return url;
    },
    fileloaded: function () {
        var preview = document.querySelector('img');
        var file = document.querySelector('input[type=file]').files[0];
        var readers = new FileReader();

        //用文件名name后缀判断文件类型，用size属性判断文件大小不能超过500k ，前端直接判断的好处，免去服务器的压力。

        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test(file.name))
        {

            return alert("图片类型必须是.gif,jpeg,jpg,png中的一种");

        } else if (file.size > 40 * 1024) {

            return alert("文件太大！请上传不超过40k的图片");
        }

        readers.onloadend = function () {
            preview.src = readers.result;
        };
        readers.readAsDataURL(file);
    }
};