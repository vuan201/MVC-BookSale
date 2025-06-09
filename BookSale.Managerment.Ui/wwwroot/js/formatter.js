function dateFormatter(value, row, index)
{
    return moment(value).format("DD/MM/YYYY");
}
function moneyFormatter(value, row, index) {
    return value.toLocaleString('vi-VN', {currency:'VND', style:'currency'})
}

const maxLength = 30;
function arrayJoinFormatter(value, row, index) {
    let result = value.join(', ');

    if (result.length > maxLength) {
        result = result.slice(0, maxLength - 3) + '...';
    }
    return result;
}