#include <iostream>

class dateOfExpiry
{
private:
	uint16_t* monthOfExpiry;
	uint16_t* yearOFExpiry;
public:
	dateOfExpiry(uint16_t&, uint16_t&);

	uint16_t getMonthOfExpiry() const;
	uint16_t getYearOfExpiry() const;

	~dateOfExpiry();
};

