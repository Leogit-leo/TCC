function toastrErrorMsg(mensagem) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-left",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    Command: toastr["error"](mensagem);
}
function toastrSuccessMsg(mensagem) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    Command: toastr["success"](mensagem);
}

var FormAJX = function () {
    $(".form-ajx").each(function () {
        $(this).validate({
            submitHandler: function (form) {
                $('.loaderSite').show();
                $(form).find('button[type="submit"]').hide();
                $(form).find('.alert').html('').hide();
                $(form).ajaxSubmit({
                    success: function (data) {
                        if (data.Sucesso) {
                            if (data.LimparForm) {
                                $(form).each(function () {
                                    this.reset();
                                });
                            }
                            if (!data.Redirecionar) {
                                toastrSuccessMsg(data.Mensagem);
                                if (!data.IgnoraScrool) {
                                    $(form).animate({ scrollTop: $(form).offset().top }, 'slow');
                                }
                                if (!data.DispararFuncaoAuxiliar)
                                    $('.loaderSite').hide();
                                $(form).find('button[type="submit"]').show();
                            } else {
                                if (data.Link == "reload") {
                                    location.reload();
                                }
                                else
                                    location.href = data.Link;
                            }
                        } else {
                            toastrErrorMsg(data.Mensagem)
                            if (!data.IgnoraScrool)
                                $('html,body').animate({ scrollTop: $(form).offset().top }, 'slow');
                            if (!data.DispararFuncaoAuxiliar)
                                $('.loaderSite').hide();
                            $(form).find('button[type="submit"]').show();
                        }

                        if (data.DispararFuncaoAuxiliar)
                            window[data.FuncaoAuxiliar](data.FuncaoAuxiliarParametro);
                    },
                    error: function (request, status, error) {
                        $('.loaderSite').hide();
                        $(form).find('button[type="submit"]').show();
                        toastrErrorMsg("Erro interno, por favor contate o administrador.")
                        console.log(request);
                    }
                });
            }
        });
    });
};

FormAJX();