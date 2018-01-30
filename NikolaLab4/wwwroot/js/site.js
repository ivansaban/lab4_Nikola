// Write your JavaScript code.
function getStores() {
    var data = {
        firstName: 'Gustavo',
        phone: '398-555-0132'
    };

    var id = document.getElementById('id').value;

    if (!id) {
        $.ajax({
            type: 'get',
            url: '/api/Stores',
            success: function (result, jqXHR) {
                $('#table').empty();
                $('#table').append('<tr><th>Name</th><th>Id</th><th>Bonus</th></tr>');
                var i;
                $(function () {
                    $.each(result, function (i, item) {
                        if (item.salesPerson) {
                            var $tr = $('<tr>').append(
                                $('<td>').text(item.name),
                                $('<td>').text(item.businessEntityId),
                                $('<td>').text(item.person.firstName)
                            ).appendTo('#table');
                        } else {
                            var $tr = $('<tr>').append(
                                $('<td>').text(item.name),
                                $('<td>').text(item.businessEntityId),
                                $('<td>').text("")
                            ).appendTo('#table');
                        }
                    });
                });
            }
        });
    } else {
        $.ajax({
            type: 'post',
            url: '/api/Token/RequestToken?api-version=1.0',
            contentType: "application/json;",
            data: JSON.stringify(data),
            success: function (result, jqXHR) {
                $.ajax({
                    type: 'get',
                    url: '/api/Stores/' + id,
                    beforeSend: function (request) {
                        request.setRequestHeader('Authorization', 'bearer ' + result);
                    },
                    success: function (result, jqXHR) {
                        $('#table').empty();
                        $('#table').append('<tr><th>Name</th><th>Id</th><th>Bonus</th></tr>');

                        if (result.salesPerson) {
                            var $tr = $('<tr>').append(
                                $('<td>').text(result.name),
                                $('<td>').text(result.businessEntityId),
                                $('<td>').text(result.person.firstName)
                            ).appendTo('#table');
                        } else {
                            var $tr = $('<tr>').append(
                                $('<td>').text(result.name),
                                $('<td>').text(result.businessEntityId),
                                $('<td>').text("")
                            ).appendTo('#table');
                        }
                    },
                    error: function (result, jqXHR) {
                        $('#table').empty();
                    }
                });
            }
        });
    }
}

function getCreditCards() {
    $.ajax({
        type: 'get',
        url: '/api/CreditCards',
        headers: {
            'api-version': '2.0'
        },
        success: function (result, jqXHR) {
            $('#table').empty();
            $('#table').append('<tr><th>Credit Card Id</th><th>Card Type</th><th>Card Number</th></tr>');
            var i;
            $(function () {
                $.each(result, function (i, item) {
                    var $tr = $('<tr>').append(
                        $('<td>').text(item.creditCardId),
                        $('<td>').text(item.cardType),
                        $('<td>').text(item.cardNumber)
                    ).appendTo('#table');
                });
            });
        }
    });
}