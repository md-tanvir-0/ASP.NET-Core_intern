-- Create Procedure: Insert a new course
CREATE OR REPLACE PROCEDURE insert_course(
    p_name TEXT,
    p_description TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO "Courses"("Name", "Description")
    VALUES (p_name, p_description);
END $$;

-- Read Procedure: Get course by ID
CREATE OR REPLACE FUNCTION get_course_by_id(p_id INT)
RETURNS SETOF "Courses"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY SELECT * FROM "Courses" WHERE "Id" = p_id;
END $$;

CREATE OR REPLACE FUNCTION get_all_courses()
RETURNS SETOF "Courses"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY SELECT * FROM "Courses";
END $$;

-- Update Procedure: Update course details
CREATE OR REPLACE PROCEDURE update_course(
    p_id INT,
    p_name TEXT,
    p_description TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "Courses"
    SET "Name" = p_name,
        "Description" = p_description
    WHERE "Id" = p_id;
END $$;
-- Delete Procedure: Delete course by ID
CREATE OR REPLACE PROCEDURE delete_course(p_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM "Courses" WHERE "Id" = p_id;
END $$;