$(function () {

    $('a.shopDetailsLink').click(loadProductsForShop);
    $('a.shopDeleteLink').click(deleteShop);

    function loadProductsForShop() {
        var uri = 'api/shops/getproducts';
        var shopid = $(this).data('shopid');

        $('tr.selectedShop').toggleClass('selectedShop', false);
        $('#shopid_' + shopid).toggleClass('selectedShop', true);

        $.getJSON(uri + '/' + shopid)
            .done(function (data) {

                var tbody = $('#tbodyDetails');
                tbody.empty();

                $.each(data, function (key, item) {                    
                    var trow = tbody.append('<tr />').children('tr:last');

                    trow.append("<td/>").children('td:last')
                     .append(key + 1);

                    trow.append("<td/>").children('td:last')
                     .append(item.Name);

                    trow.append("<td/>").children('td:last')
                      .append(item.Description);
                });                                   
            })
            .fail(function (jqXHR, textStatus, err) {
                $('#errorLine').text('Error: ' + err);
            });
    }    

    function deleteShop() {
        var uri = 'api/shops/deleteshop';
        var shopid = $(this).data('shopid');

        deletePost();        

        function deletePost() {
            $.ajax({
                url: uri + '/' + shopid,
                type: "POST",
                success: function (data) {
                    var row = $('#shopid_' + shopid);
                    row.fadeOut(400, function () {
                        row.remove();
                    });

                    var tbody = $('#tbodyDetails');
                    tbody.fadeOut(400, function () {
                        tbody.empty();
                    });                    
                },
                error: function (jqXHR, textStatus, err) {
                    $('#errorLine').text('Error: ' + err);
                }
            });
        };
    }

});