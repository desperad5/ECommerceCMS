export interface ICrudService {
	fetchAll();
	createOrEdit(model: any);
	deleteById(id: number);
}
