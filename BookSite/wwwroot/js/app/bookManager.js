const bookManager = {
    /**
     * Save a new book related to the current user
     * @param {Object} book  A book object
     * @returns {Promise} axious request to Book API
     */
    saveBook: function (book) {
        return axios.post("/api/books", book);
    },

    /**
     * Get a list of blooks managed by the current user
     * @param {number} authorId Optional ID to narrow list to specific Author
     * @returns {book[]} Array of books
     */
    getMyBooks: function (authorId) {
        return axios.get("/api/books/mine" +(authorId ? `/${authorId}` : ""));
    },

    /**
     * Get a list of series associated with books managed by current user
     * @param {number} authorId  Optional ID to narrow list to specific Author
     * @returns {series[]} Array of series
     */
    getMySeries: function (authorId) {
        return axios.get("/api/series/mine" + (authorId ? `/${authorId}` : ""));
    },

    /**
     * Get a specific book by its ID
     * @param {number} bookId required ID
     * @returns {object} book
     */
    getBookById: function (bookId) {
        return axios.get(`/api/books/${bookId}`);
    },

    authorList: function(authors) {
        return authors.reduce(function (authorList, bookAuthor, index, authorsArray) {
            if (index === 0) {
                return bookAuthor.author.penName;
            } else if (index === authorsArray.length - 1) {
                return `${authorList} and ${bookAuthor.author.penName}`;
            } else {
                return `${authorList}, ${bookAuthor.author.penName}`;
            }
        }, "");
    },

    uploadCoverArt(bookId, formData) {
        return axios.post(`/api/books/coverart/${bookId}`, formData);
    },
    updateCoverSizings(cover) {
        return axios.put(`/api/books/coverartsizings/${cover.id}/${cover.spineWidth}/${cover.insideFlapWidth}`);
    },

    updateBook(book) {
        book.coverArtSpineWidth = parseFloat(book.coverArtSpineWidth);
        return axios.put(`/api/books/${book.id}`, book);
    },

    addUpdateSection(bookId, formData, sectionId) {
        return axios.post(`/api/books/section/${bookId}${sectionId ? '/' + sectionId : ''}`, formData);
    },
    updateSectionOrder(bookId, sections) {
        return axios.put(`/api/books/sectionorder/${bookId}`, sections);
    },
    addUpdateBlurb(bookId, blurb) {
        return axios.post(`/api/books/blurb/${bookId}`, blurb);
    },
    addUpdateEdition(bookId, edition) {
        return axios.post(`/api/books/edition/${bookId}`, edition);
    }


};