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
        var fileObj = document.getElementById(id).files[0];
        if (window.createObjcectURL !== undefined) {
            url = window.createOjcectURL(fileObj);
        } else if (window.URL !== undefined) {
            url = window.URL.createObjectURL(fileObj);
        } else if (window.webkitURL !== undefined) {
            url = window.webkitURL.createObjectURL(fileObj);
        }
        return url;
    }
};