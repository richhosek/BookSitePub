const authorManager = {
    /**
     * Save a new author related to the current user
     * @param {Object} author  An author object
     * @returns {Promise} axious request to Author API
     */
    saveAuthor: function (author) {
        return axios.post("/api/authors", author);
    },

    /**
     * Get a list of authors managed by the current user
     * @returns {author[]} Array of authors
     */
    getMyAuthors: function () {
        return axios.get("/api/authors/mine");
    }
};