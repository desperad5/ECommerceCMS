export class MenuConfig {
	public defaults: any = {
		header: {
			self: {},
			items: [
				{
					title: 'Dashboards',
					root: true,
					alignment: 'left',
					page: '/dashboard',
					translate: 'MENU.DASHBOARD',
				},
				{
					title: 'Tenant',
					root: true,
					alignment: 'left',
					page: '/tenant',
					translate: 'MENU.TENANT',
					admin:true
				},
				{
					title: 'Create User',
					root: true,
					alignment: 'left',
					page: '/user-registration',
					translate: 'MENU.CREATE_USER',
					admin: true
				},
				{
					title: 'Listings',
					root: true,
					alignment: 'left',
					page: '/listing',
					translate: 'MENU.LISTINGS'
				},
				{
					title: 'Product Category',
					root: true,
					alignment: 'left',
					page: '/productcategory',
					translate: 'MENU.PRODUCTCATEGORY',
					admin: true
				},
				{
					title: 'Brands',
					root: true,
					alignment: 'left',
					page: '/brand',
					translate: 'MENU.BRANDS'
				}
				
				
				//{
				//	title: 'Bundle',
				//	root: true,
				//	alignment: 'left',
				//	page: '/bundle',
				//	translate: 'MENU.BUNDLE',
				//}
			]
		},
		aside: {
			self: {},
			items: [
				{
					title: 'Dashboards',
					root: true,
					icon: 'flaticon2-open-text-book',
					alignment: 'left',
					page: '/dashboard',
					translate: 'MENU.DASHBOARD',
				},
				{
					title: 'Tenant',
					root: true,
					icon: 'flaticon2-architecture-and-city',
					page: '/tenant',
					translate: 'MENU.TENANT',
					bullet: 'dot',
					admin: true
				},
				
				{
					title: 'Create User',
					root: true,
					alignment: 'left',
					icon: 'flaticon-user-settings',
					page: '/user-registration',
					translate: 'MENU.CREATE_USER',
					admin: true
				},
				{
					title: 'Listings',
					root: true,
					alignment: 'left',
					icon: 'flaticon-price-tag',
					page: '/listing',
					translate: 'MENU.LISTINGS'
				},
				{
					title: 'Product Category',
					root: true,
					alignment: 'left',
					icon: 'flaticon-user-settings',
					page: '/productcategory',
					translate: 'MENU.PRODUCTCATEGORY',
					admin: true
				},
				{
					title: 'Brands',
					root: true,
					alignment: 'left',
					icon: 'flaticon-price-tag',
					page: '/brand',
					translate: 'MENU.BRANDS'
				}
				
				//{
				//	title: 'Question Card',
				//	root: true,
				//	alignment: 'left',
				//	icon: 'flaticon-folder-1',
				//	page: '/card/question',
				//	translate: 'MENU.QUESTION_CARD',
				//},
				//{
				//	title: 'Exam Card',
				//	root: true,
				//	alignment: 'left',
				//	icon: 'flaticon-file-2',
				//	page: '/card/exam',
				//	translate: 'MENU.EXAM_CARD',
				//},
				//{
				//	title: 'Topic Card',
				//	root: true,
				//	alignment: 'left',
				//	icon: 'flaticon-notes',
				//	page: '/card/topic',
				//	translate: 'MENU.TOPIC_CARD',
				//},
				//{
				//	title: 'Bundle',
				//	root: true,
				//	alignment: 'left',
				//	icon: 'flaticon-open-box',
				//	page: '/bundle',
				//	translate: 'MENU.BUNDLE',
				//}
				
			]
		},
	};

	public get configs(): any {
		return this.defaults;
	}
}
