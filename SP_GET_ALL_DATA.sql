SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maicol Rozo>
-- Create date: <16/10/2025>
-- Description:	<Get all the data employee>
-- =============================================
CREATE PROCEDURE SP_GET_ALL_EMPLOYEE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT * FROM dbo.Employees E
	LEFT JOIN dbo.EmployeeAddresses A ON E.ID = A.EmployeeID
	LEFT JOIN dbo.EmployeePhoneNumbers P ON E.ID = p.EmployeeID
END
GO
