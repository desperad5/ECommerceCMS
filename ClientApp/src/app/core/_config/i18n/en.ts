// USA
export const locale = {
	lang: 'en',
	data: {
		TRANSLATOR: {
			SELECT: 'Select your language',
		},
		MENU: {
			NEW: 'new',
			ACTIONS: 'Actions',
			CREATE_POST: 'Create New Post',
			PAGES: 'Pages',
			FEATURES: 'Features',
			APPS: 'Apps',
			DASHBOARD: 'Dashboard',
			TENANT: "Tenant Operations",
			LESSON: "Lesson Operations",
			TOPIC: "Topic Operations",
			FILTER: "Filter",
			SEARCHONTABLE: "Search on table",
			QUESTION_CARD: "Question Card",
			EXAM_CARD: "Exam Card",
			TOPIC_CARD: "Topic Card",
			CREATE_USER: "User Management",
			BUNDLE: "Bundle Management",
			BRANDS: "Markalar"
		},
		AUTH: {
			GENERAL: {
				OR: 'Or',
				SUBMIT_BUTTON: 'Submit',
				NO_ACCOUNT: 'Don\'t have an account?',
				SIGNUP_BUTTON: 'Sign Up',
				FORGOT_BUTTON: 'Forgot Password',
				BACK_BUTTON: 'Back',
				PRIVACY: 'Privacy',
				LEGAL: 'Legal',
				CONTACT: 'Contact',
				RESET_PASSWORD: "Reset Password"
			},
			LOGIN: {
				TITLE: 'Login Account',
				BUTTON: 'Sign In',
			},
			FORGOT: {
				TITLE: 'Forgotten Password?',
				DESC: 'Enter your email to reset your password',
				SUCCESS: 'Your account has been successfully reset.'
			},
			REGISTER: {
				TITLE: 'Sign Up',
				DESC: 'Enter your details to create your account',
				SUCCESS: 'Your account has been successfuly registered.'
			},
			INPUT: {
				EMAIL: 'Email',
				FULLNAME: 'Fullname',
				PASSWORD: 'Password',
				CONFIRM_PASSWORD: 'Confirm Password',
				USERNAME: 'Username'
			},
			VALIDATION: {
				INVALID: '{{name}} is not valid',
				REQUIRED: '{{name}} is required',
				MIN_LENGTH: '{{name}} minimum length is {{min}}',
				AGREEMENT_REQUIRED: 'Accepting terms & conditions are required',
				NOT_FOUND: 'The requested {{name}} is not found',
				INVALID_LOGIN: 'The login detail is incorrect',
				REQUIRED_FIELD: 'Required field',
				MIN_LENGTH_FIELD: 'Minimum field length:',
				MAX_LENGTH_FIELD: 'Maximum field length:',
				INVALID_FIELD: 'Field is not valid',
				CONFIRM_VALIDATION: "Passsword and ConfirmPassword didn't match."
			}
		},
		ECOMMERCE: {
			COMMON: {
				SELECTED_RECORDS_COUNT: 'Selected records count: ',
				ALL: 'All',
				SUSPENDED: 'Suspended',
				ACTIVE: 'Active',
				FILTER: 'Filter',
				BY_STATUS: 'by Status',
				BY_TYPE: 'by Type',
				BUSINESS: 'Business',
				INDIVIDUAL: 'Individual',
				SEARCH: 'Search',
				IN_ALL_FIELDS: 'in all fields',
				CLEAR: "Clear",
				NOENTRIESFOUND: "No Entries found on label"
			},
			ECOMMERCE: 'eCommerce',
			CUSTOMERS: {
				CUSTOMERS: 'Customers',
				CUSTOMERS_LIST: 'Customers list',
				NEW_CUSTOMER: 'New Customer',
				DELETE_CUSTOMER_SIMPLE: {
					TITLE: 'Customer Delete',
					DESCRIPTION: 'Are you sure to permanently delete this customer?',
					WAIT_DESCRIPTION: 'Customer is deleting...',
					MESSAGE: 'Customer has been deleted'
				},
				DELETE_CUSTOMER_MULTY: {
					TITLE: 'Customers Delete',
					DESCRIPTION: 'Are you sure to permanently delete selected customers?',
					WAIT_DESCRIPTION: 'Customers are deleting...',
					MESSAGE: 'Selected customers have been deleted'
				},
				UPDATE_STATUS: {
					TITLE: 'Status has been updated for selected customers',
					MESSAGE: 'Selected customers status have successfully been updated'
				},
				EDIT: {
					UPDATE_MESSAGE: 'Customer has been updated',
					ADD_MESSAGE: 'Customer has been created'
				}
			}

		},
		TENANT: {
			NEW_TENANT: "New Tenant",
			CREATE_NEWTENANT: "Create New Tenant",
			TENANT_LIST: "Tenant List",
			NAME_COLUMN: "Name",
			CREATEDDATE_COLUMN: "Creation Date",
			ADDRESS_COLUMN: "Address",
			COUNTRY_COLUMN: "Country",
			TOWN_COLUMN: "Town",
			TYPEID_COLUMN: "Type",
			PHONENUMBER_COLUMN: "Phone Number",
			TAX_COLUMN: "Tax Administration",
			TAXNUMBER_COLUMN: "Tax Number"
		},
		LESSON: {
			LESSON: "Lesson",
			NEW_LESSON: "New Lesson",
			CREATE_NEWLESSON: "Yeni Ders Oluştur",
			LESSON_LIST: "Lesson List",
			EDIT: {
				UPDATE_MESSAGE: 'Lesson has been updated',
				ADD_MESSAGE: 'Lesson has been created'
			},
			NAME_COLUMN: "Name",
			CREATED_DATE_COLUMN: "Created Date",
			EDUCATION_LEVEL_COLUMN: "Education Level",
			TOPICS_COLUMN: "Topics",
			ACTIVE_COLUMN: "Active",
			ACTIONS_COLUMN: "Actions"

		},
		TOPIC: {
			NEW_TOPIC: "New Topic",
			TOPIC_LIST: "Topic List",
			SAVE_MESSAGE: "New Topic is Saved",
			EDIT_MESSAGE: "Topic is Updated",
			DELETE_MESSAGE: "Topic is Deleted",
			DELETE: "Delete",
			EDIT: "Edit",
			EDIT_TOPIC: "Edit Topic",
			CREATE_NEW_TOPIC: "Create New Topic",
			TOPIC_NAME_IS_REQUIRED: "Topic Name is Required",
			LESSON_IS_REQUIRED: "Lesson is Required.",
			ENTER_TOPIC_NAME: "Please Enter Topic Name",
			ENTER_CLASS_LEVEL: "Please Enter Class Level",
			CLASS_LEVEL_REQUIRED: "Class Level must be between 1 and 12",
			SELECT_PARENT_TOPIC: "Select Parent Topic",
			SELECT_LESSON: "Select Lesson",
			CANCEL: "Cancel",
			SAVE: "Save",
			TABLE: {
				ID: "ID",
				NAME: "Topic Name",
				LESSON_NAME: "Lesson Name",
				CLASS_LEVEL: "Class Level",
				PARENT_TOPIC_NAME: "Parent Topic Name",
				ACTIONS: "Actions"
			},
			DELETE_DIALOG: {
				INFO_MESSAGE: 'Delete Topic',
				CONFIRM_MESSAGE: 'Topic will be deleted. Are you sure?',
				WAITING_MESSAGE: 'Deleting...'
			},
		},
		BRAND: {
			NEW_BRAND: "New Brand",
			BRAND_LIST: "Brand List",
			SAVE_MESSAGE: "New Brand is Saved",
			EDIT_MESSAGE: "Brand is Updated",
			DELETE_MESSAGE: "Brand is Deleted",
			DELETE: "Delete",
			EDIT: "Edit",
			EDIT_BRAND: "Edit Brand",
			CREATE_NEW_BRAND: "Create New Brand",
			BRAND_NAME_IS_REQUIRED: "Brand Name is Required",
			ENTER_BRAND_NAME: "Please Enter Brand Name",
			ENTER_WEB_SITE_URL: "Please Enter Web Site Url",
			CANCEL: "Cancel",
			SAVE: "Save",
			TABLE: {
				ID: "ID",
				NAME: "Brand Name",
				WEB_SITE_URL:"Web Site",
				ACTIONS: "Actions"
			},
			DELETE_DIALOG: {
				INFO_MESSAGE: 'Delete Brand',
				CONFIRM_MESSAGE: 'Brand will be deleted. Are you sure?',
				WAITING_MESSAGE: 'Deleting...'
			},
		},
		QUESTIONCARD: {
			NEW_QUESTIONCARD: "New Question Card",
			CREATE_NEW_QUESTIONCARD: "Create new question card",
			NAME_COLUMN: "Name",
			CREATED_DATE_COLUMN: "Created Date",
			DESCRIPTION: "Description",
			TENANT: "Tenant",
			TOPIC: "Topic",
			LESSON: "Lesson",
			QUESTION_COUNT: "Number Of Questions",
			FILE_URL: " File Url",
			PRICE: "Price",
			ACTIVE_COLUMN: "Active",
			ACTIONS_COLUMN: "Actions",
			EDIT_QUESTIONCARD: "Edit Question Card",
			ADD_QUESTIONCARD: "Add Question Card",
			QUESTIONCARD_LIST: "Question Card List",
			ENTER_CARD_NAME: "Enter Card Name",
			ENTER_DESCRIPTION: "Enter Description",
			ENTER_TENANT: "Enter Tenant",
			ENTER_LESSON: "Enter Lesson",
			ENTER_PRICE: "Enter Price",
			ENTER_TOPIC: "Enter Topic",
			EDIT: {
				UPDATE_MESSAGE: 'Question Card has been updated',
				ADD_MESSAGE: 'Question Card has been created'
			},
			DELETE: {
				INFO_MESSAGE: 'Delete Question Card',
				CONFIRM_MESSAGE: 'Question Card will be deleted. Are you sure?',
				WAITING_MESSAGE: 'Deleting...'
			}
		},
		EXAMCARD: {
			NEW_EXAMCARD: "New Exam Card",
			CREATE_NEW_EXAMCARD: "Create New Exam Card",
			TYPE: " Exam Type",
			ENTER_TYPE: "Enter Exam Type",
			CREATE_NEW_TOPICCARD: "Create New Topic Card",
			NAME_COLUMN: "Name",
			CREATED_DATE_COLUMN: "Created Date",
			DESCRIPTION: "Description",
			TENANT: "Tenant",
			QUESTION_COUNT: "Number Of Questions",
			FILE_URL: " File Url",
			PRICE: "Price",
			ACTIVE_COLUMN: "Active",
			ACTIONS_COLUMN: "Actions",
			EDIT_EXAMCARD: "Edit Exam Card",
			ADD_EXAMCARD: "Add Exam Card",
			EXAMCARD_LIST: "Exam Card List",
			ENTER_CARD_NAME: "Enter Card Name",
			ENTER_DESCRIPTION: "Enter Description",
			ENTER_TENANT: "Enter Tenant",
			ENTER_PRICE: "Enter Price",
			EDIT: {
				UPDATE_MESSAGE: 'Exam Card has been updated',
				ADD_MESSAGE: 'Exam Card has been created'
			},
			DELETE: {
				INFO_MESSAGE: 'Delete Exam Card',
				CONFIRM_MESSAGE: 'Exam Card will be deleted. Are you sure?',
				WAITING_MESSAGE: 'Deleting...'
			}
		},
		TOPICCARD: {
			NEW_TOPICCARD: "New Topic Card",
			NAME_COLUMN: "Name",
			CREATED_DATE_COLUMN: "Created Date",
			DESCRIPTION: "Description",
			TENANT: "Tenant",
			QUESTION_COUNT: "Number Of Questions",
			FILE_URL: " File Url",
			PRICE: "Price",
			ACTIVE_COLUMN: "Active",
			ACTIONS_COLUMN: "Actions",
			EDIT_TOPICCARD: "Edit Topic Card",
			ADD_TOPICCARD: "Add Topic Card",
			TOPICCARD_LIST: "Topic Card List",
			ENTER_CARD_NAME: "Enter Card Name",
			ENTER_DESCRIPTION: "Enter Description",
			ENTER_TENANT: "Enter Tenant",
			ENTER_PRICE: "Enter Price",
			TOPIC: "Topic",
			LESSON: "Lesson",
			ENTER_LESSON: "Enter Lesson",
			ENTER_TOPIC: "Enter Topic",
			EDIT: {
				UPDATE_MESSAGE: 'Topic Card has been updated',
				ADD_MESSAGE: 'Topic Card has been created'
			},
			DELETE: {
				INFO_MESSAGE: 'Delete Topic Card',
				CONFIRM_MESSAGE: 'Topic Card will be deleted. Are you sure?',
				WAITING_MESSAGE: 'Deleting...'
			},
		},
		ENTER_LOGO_NAME: "Enter logo name.",
		ENTER_TENANT_NAME: "Enter tenant name.",
		TENANT_NAME: "Tenant name",
		LESSON_NAME: "Lesson Name",
		ENTER_LESSON_NAME: "Enter lesson name",
		ENTER_COUNTY: "Enter county name",
		COUNTY: "County is",
		ENTER_TOWN: "Enter town name",
		ENTER_ADDRESS: "Enter address.",
		ENTER_PHONE_NUMBER: "Enter phone number.",
		PHONE_NUMBER: "Phone Number",
		TYPE: "Type",
		ADDRESS: "Adress",
		SELECT_TYPE: "Select type",
		ENTER_TAX_NUMBER: "Enter tax number.",
		TAX_NUMBER: "Tax number",
		ENTER_TAX_ADMINISTRATION: "Enter tax administration name.",
		TAX_ADMINISTRATION: "Tax Administration",
		REQUIRED: "required",
		PUBLIC_SECTOR: "Public Sector",
		PRIVATE_SECTOR: "Private Sector",
		LOGO_NAME: "Logo Name",
		TOWN: "Town",
		SAVE: "Save",
		CANCEL: "Cancel",
		EDIT_TENANT: "Edit Tenant",
		ADD_TENANT: "Add Tenant",
		EDIT_USER: "Edit User",
		ADD_USER: "Add User",
		DELETE: "Delete",
		EDIT: "Edit",
		DETAILS: "Details",
		EDIT_LESSON: "Edit Lesson",
		ADD_LESSON: "Add Lesson",
		ENTER_EDUCATION_LEVEL_NAME: "Enter Education Level:",
		IS_ACTIVE: "Active",
		ENTER_ACTIVE_NAME: "Active",
		NEW_USER: "New User",
		USER_LIST: "User List",
		SELECTED_RECORDS_COUNT: "Selected records count: ",
		CHANGE_PASSWORD: "Change Password",
		NAME: "Name",
		SURNAME: "Surname",
		EMAIL: "Email",
		USER_NAME: "User Name",
		ACTIONS: "Actions",
		FILE_URL: "File",
		PASSWORD_CHANGE_SUCCESS: "Your password has been changed successfully.",
		CLOSE: "Close",
		DATATABLE: {
			SEARCH: "Search on table"
		},
		NEW_BUNDLE: "New Bundle",
		CREATE_NEW_BUNDLE:"Create New Bundle",
		BUNDLE_LIST: "Bundle List",
		ADD_BUNDLE: "Add Bundle",
		EDIT_BUNDLE: "Edit Bundle",
		UPDATE_MESSAGE: "Record Updated.",
		ADD_MESSAGE: "Record Added.",
		USERADD_MESSAGE: "User Added",
		DESCRIPTION: "Description",
		PRICE: "Price",
		FORM_ERROR: "Oh snap! Change a few things up and try submitting again.",
		INVALID_COMBINATION: "Invalıd Email Address or Password",
		SIGN_OUT: "Sign Out",
		CURRENCY_TYPE: "$",
		MOREACTIONS:"More Actions"


	}
};
