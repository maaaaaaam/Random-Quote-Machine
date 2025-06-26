document.getElementById('import').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const file = document.querySelector('#import > input').files[0];

    if (!file) {
        alert("No file detected");
        return
    }

    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch('http://localhost:5142/api/quotes', {
            method: 'POST',
            body: formData
        });

        if (response.ok) {alert('Import successful')}
        else {alert('Import failed')}
    } catch (err) {
        alert('Error' + err.message)
    }
})