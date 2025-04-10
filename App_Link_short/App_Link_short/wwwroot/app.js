window.copyToClipboard = async (textCopy) => {
    if (!navigator.clipboard || !navigator.clipboard.writeText) {
        console.error('Clipboard API is not supported in this browser');
        return false; // Trả về false để báo thất bại
    }

    try {
        await navigator.clipboard.writeText(textCopy);
        console.log('Text copied to clipboard successfully');
        return true; // Trả về true để báo thành công
    } catch (e) {
        console.error('Error while copying to clipboard:', e);
        return false; // Trả về false nếu có lỗi
    }
};