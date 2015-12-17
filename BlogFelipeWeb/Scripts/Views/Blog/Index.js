$(document).ready(function () {
    //alert('Olá, este é um alert do JavaScript. ');
    $('.excluir-post').on('click', function (e) {
        if (!confirm('Deseja realmente excluir esse post?')) {
            e.preventDefault();
        }
    });

        //alert('Olá, este é um alert do JavaScript. ');
        $('.excluir-comentario').on('click', function (e) {
            if (!confirm('Deseja realmente excluir esse Comentario?')) {
                e.preventDefault();
            }
        });
});