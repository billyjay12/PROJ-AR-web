var ns4;

function Cancel() {
    var isCancelled = confirm("ARE YOU SURE YOU WANT TO CANCEL?");
    if (isCancelled) {
        window.history.back()
    }
}