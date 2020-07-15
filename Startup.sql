USE blogs252;

-- CREATE TABLE blogs
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     author VARCHAR(255) NOT NULL,
--     body VARCHAR(255),
--     title VARCHAR(255),
--     PRIMARY KEY (id)
-- );

CREATE TABLE comments
(
    id INT NOT NULL AUTO_INCREMENT,
    author VARCHAR(255) NOT NULL,
    body VARCHAR(255),
    blogId INT NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (blogId)
        REFERENCES blogs (id)
        ON DELETE CASCADE
        
)
--         ,
--         FOREIGN KEY (author)
--             REFERENCES profiles (email)
--             ON DELETE CASCADE
-- )

-- CREATE TABLE profiles
-- (
--     email VARCHAR(255) NOT NULL UNIQUE,
--     name VARCHAR(255),


--     PRIMARY KEY (email)
-- )