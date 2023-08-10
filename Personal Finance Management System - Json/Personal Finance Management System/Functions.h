#include "wallet.h" 
#include <fstream>
#include <regex>
#pragma once

namespace functions
{
	template<typename T>
	void saveInFile(T**& data, std::string fileName, uint16_t count)
	{
		fileName += ".json";

		json* jsonData = new json[count];
		for (size_t i = 0; i < count; i++)
		{
			jsonData[i] << *data[i];
		}

		std::ofstream outputFile(fileName);

		if (!outputFile.is_open()) {
			std::cerr << "Error" << std::endl;
			return;
		}

		for (size_t i = 0; i < count; i++)
		{
			outputFile << jsonData[i];
		}

		outputFile.close();
		delete[] jsonData;
	}

	template <typename T>
	bool loadFromFile(T**& downloadData, std::string fileName, uint16_t& count)
	{
		fileName += ".json";

		std::ifstream inputFile(fileName);

		if (!inputFile.is_open()) {
			return false;
		}

		json inputData;

		try {
		for (int i{}; inputFile >> inputData; i++)
		{
			T data; 
			inputData >> data;
			downloadData[i] = new T(data); 
			count++;
		}
		}
		catch (...) {};
		inputFile.close();

		return true;
	}

	std::string checkWallets(wallet** _wallet, uint16_t& count);
	void formationOfRatings(wallet* wallets);
	void top3cost(wallet* wallets);
	void top3category(wallet* wallets);
	int getCurrentYear();
	void myCheck(std::string& str, std::regex regexCheck);
	wallet* addWallet();
	Transaction* addTransaction();
	personalData* addPersonalData();
	dateOfExpiry* addDateOfExpiry();
};