document.addEventListener('DOMContentLoaded', function () {
    const searchButton = document.getElementById('search');
    const searchBox = document.getElementById('search-box');

    let isOpen = false;

    searchButton.addEventListener('click', function (event) {
        isOpen = !isOpen;
        if (isOpen) {
            searchBox.classList.add('show');
        } else {
            searchBox.classList.remove('show');
        }
        event.stopPropagation(); // Prevents the click event from propagating to the document
    });

    // Close the search box when clicking outside of it
    document.addEventListener('click', function (event) {
        if (!searchButton.contains(event.target) && !searchBox.contains(event.target)) {
            searchBox.classList.remove('show');
            isOpen = false;
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const searchButton = document.getElementById('searchButton');

    if (searchButton) {
        searchButton.addEventListener('click', function (event) {
            // Prevent the default form submission behavior
            event.preventDefault();

            // Submit the form
            document.getElementById("searchSubmitForm").submit();
        });
    }
});