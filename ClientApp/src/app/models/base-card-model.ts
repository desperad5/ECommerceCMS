import { TenantModel } from './tenant-model';

export class BaseCardModel {
	id: number;
	tenant: TenantModel;
	isActive: boolean;
	price: number;
	name: string;
	description: string;
	fileUrl: string;
	productId: number;
}
